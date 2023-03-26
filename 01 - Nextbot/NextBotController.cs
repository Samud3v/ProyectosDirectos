using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NextBotController : MonoBehaviour
{
    // next bot controller
    enum NextBotState
    {
        STALK, // 0
        CHASE, // 1
        HIDE // 2
    }

    [SerializeField]
    private float stalkspeed = 10.0f;
    [SerializeField]
    private float chasespeed = 18.0f;
    [SerializeField]
    private NextBotState mode = NextBotState.STALK;
    [SerializeField]
    private float stalkarea = 5.0f;
    [SerializeField]
    private float hidearea = 50.0f;
    [SerializeField]
    private float distanceToChase = 3.0f;
    [SerializeField]
    private float killDistance = 1.0f;
    [SerializeField]
    private float maxChaseTime = 5.0f;
    [SerializeField]
    private float chaseTime = 0.0f;
    [SerializeField]
    private Vector3 hidePosition;

    NavMeshAgent agent;
    public SpriteRenderer sprite;
    public AudioSource chaseAudio;
    public bool killedThePlayer = false;
    public Canvas deadCanvas;
    GameObject player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        chaseTime = maxChaseTime;
    }

    void Update(){
        Debug.Log(mode);
        
        switch(mode){
            case NextBotState.STALK:
                Stalking();
                break;
            case NextBotState.CHASE:
                chasing();
                break;
            case NextBotState.HIDE:
                hiding();
                break;
            default:
                Debug.Log("Error: NO SE COMO HAS LLEGADO AQUI");
                break;
        }
    }

    private void Stalking()
    {
        agent.speed = stalkspeed;
        if(chaseAudio.isPlaying) chaseAudio.Stop();
        // el mostro se acerca al jugador en un radio
        agent.SetDestination(player.transform.position);

        if(Vector3.Distance(transform.position, player.transform.position) < distanceToChase){
            mode = NextBotState.CHASE;
        }

        // linecast para ver si el jugador esta en el campo de vision
        RaycastHit hit;
        if(Physics.Linecast(transform.position, player.transform.position, out hit)){
            if(sprite.isVisible){
                if(hit.collider.gameObject == player){
                    switch(Random.Range(0, 4)){
                        case 0: // perseguir
                            mode = NextBotState.CHASE;
                        break;
                        default: // esconderse
                            // hidePosition = Random.insideUnitSphere * hidearea;
                            // hidePosition.y = 0;
                            hidePosition = new Vector3(Random.Range(-hidearea, hidearea), 0, Random.Range(-hidearea, hidearea));
                            agent.SetDestination(hidePosition);
                            mode = NextBotState.HIDE;
                        break;
                    }
                }
            }
        }
    }

        private void chasing()
    {
        agent.speed = chasespeed;
        if(!chaseAudio.isPlaying) chaseAudio.Play();

        agent.SetDestination(player.transform.position);

        if(Vector3.Distance(transform.position, player.transform.position) < killDistance){
            killedThePlayer = true;
            deadCanvas.enabled = true;
            GameObject.Find("BackGroundNoise").GetComponent<AudioSource>().Stop();
            if(chaseAudio.isPlaying) chaseAudio.Stop();
            Destroy(GameObject.Find("Player"));
            Destroy(this.gameObject);
        }

        chaseTime -= Time.deltaTime;
        if(chaseTime <= 0){
            chaseTime = maxChaseTime;
            transform.position = player.transform.position + Random.insideUnitSphere * hidearea;
            //transform.position = new Vector3(UnityEngine.Random.Range(-hidearea, hidearea), 0, UnityEngine.Random.Range(-hidearea, hidearea));
            mode = NextBotState.STALK;
        }
    }

    private void hiding()
    {
        if(chaseAudio.isPlaying) chaseAudio.Stop();

        if(Vector3.Distance(transform.position, hidePosition) < 1.0f){
            mode = NextBotState.STALK;
        }
    }
}
