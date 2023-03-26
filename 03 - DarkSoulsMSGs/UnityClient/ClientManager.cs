using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Text;
using System;
using Newtonsoft.Json.Linq;

public class ClientManager : MonoBehaviour
{
    TcpClient client;
    NetworkStream stream ;
    public string serverHost = "localhost";
    public int serverPort = 8124;
    public Vector3 vector3Data = new Vector3(1.0f, 2.0f, 3.0f);
    public string messageData = "Hello, server!";

    [System.Serializable]
    public class messageStruct {
        [SerializeField]
        public Vector3 vector3;
        [SerializeField]
        public string message;
    }

    public List<messageStruct> messageList = new List<messageStruct>();

    public GameObject bottle;

    Transform player;

    public TMPro.TMP_InputField inputField;
    public TMPro.TMP_Text serverStatus;
    private bool isConnected;

    void Start()
    {
        player = GameObject.Find("Player").transform;

        ConnectTCP();
        if(isConnected){
            serverStatus.text = "Connected to server";
            serverStatus.color = Color.green;
        } else {
            serverStatus.text = "Not connected to server";
            serverStatus.color = Color.red;
        }

        ReceiveData();

        SpawnBottles();

        // SendData();

        foreach(messageStruct msg in messageList){
            Debug.Log("Vector3: " + msg.vector3);
            Debug.Log("Message: " + msg.message);
        }

    }

    void OnApplicationQuit()
    {
        CloseConnection();
    }

    void ConnectTCP(){
        try {
            // Connect to the server
            client = new TcpClient();
            client.Connect(serverHost, serverPort);
            isConnected = client.Connected;
        } catch (Exception e) {
            Debug.LogError("Error connecting to server: " + e);
            return;
        }
    }

    void ReceiveData() {
        if(!isConnected) return;
        // Receive the data from the server in a json format
        byte[] data = new byte[1024];
        stream = client.GetStream();
        StringBuilder jsonDataBuilder = new StringBuilder();

        while (stream.DataAvailable) {
            // Read data in chunks
            int bytesRead = stream.Read(data, 0, data.Length);

            // Append the data to the string builder
            jsonDataBuilder.Append(Encoding.ASCII.GetString(data, 0, bytesRead));
        }

        string jsonData = jsonDataBuilder.ToString();

        Debug.Log("Received data: " + jsonData);

        // Parse the JSON data
        List<messageStruct> receivedData = new List<messageStruct>();
        try {
            JArray jsonArray = JArray.Parse(jsonData);
            foreach (JObject jsonObject in jsonArray) {
                JObject vector3Json = (JObject) jsonObject["vector3"];
                Vector3 vector3 = new Vector3(
                    (float) vector3Json["x"],
                    (float) vector3Json["y"],
                    (float) vector3Json["z"]
                );
                string message = (string) jsonObject["message"];
                receivedData.Add(new messageStruct { vector3 = vector3, message = message });
            }
        } catch (Exception e) {
            Debug.LogError("Error parsing JSON data: " + e);
            return;
        }

        // Add the received data to the message list
        messageList.AddRange(receivedData);
    }


    void SpawnBottles(){
        // Spawn the bottles
        foreach(messageStruct msg in messageList){
            GameObject newbottle = Instantiate(bottle, msg.vector3, Quaternion.identity, this.transform);
            newbottle.GetComponent<bottleController>().UpdateInfo(msg.message, msg.vector3);
        }
    }

    public void SendData(){
        if(!isConnected) return;
        vector3Data = player.position;
        messageData = inputField.text;

        if(messageData == ""){
            return;
        }

        Debug.Log("Sending data: " + messageData + " at " + vector3Data + "");

        // Convert the Vector3 data to a JSON object
        string jsonData = "{\"vector3\":{" +
            "\"x\":" + Mathf.Floor(vector3Data.x) + "," +
            "\"y\":" + Mathf.Floor(vector3Data.y + 1) + "," +
            "\"z\":" + Mathf.Floor(vector3Data.z) + "}," +
            "\"message\":\"" + messageData + "\"}";

        // Send the JSON data to the server
        byte[] data = Encoding.ASCII.GetBytes(jsonData);
        stream = client.GetStream();
        stream.Write(data, 0, data.Length);

        GameObject newbottle = Instantiate(bottle, vector3Data, Quaternion.identity, this.transform);
        newbottle.GetComponent<bottleController>().UpdateInfo(messageData, vector3Data);
    }

    private void CloseConnection()
    {
        if(!isConnected) return;
        stream.Close();
        client.Close();
    }

    public void Quit(){
        Application.Quit();
    }
}
