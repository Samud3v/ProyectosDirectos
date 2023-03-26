// tcp server
const net = require('net');
const fs = require('fs');

const allData = [];

const jsonFilePath = 'data.json';

// Read existing data from the JSON file
fs.readFile(jsonFilePath, 'utf8', (err, fileData) => {
    if (err) {
        console.error('Error reading JSON file:', err);
        return;
    }

    try {
        allData.push(...JSON.parse(fileData));
        console.log('Existing data loaded from JSON file:', allData);
    } catch (error) {
        console.error('Error parsing existing JSON data:', error);
    }
});

const server = net.createServer((socket) => {
    // Send the existing data to the client when it connects
    const jsonData = JSON.stringify(allData);
    socket.write(jsonData);
    
    socket.on('data', (data) => {
        // Parse incoming data as JSON
        let jsonData;
        try {
            jsonData = JSON.parse(data.toString());
        } catch (error) {
            console.error('Error parsing JSON:', error);
            return;
        }

        // Extract the Vector3 and message from the JSON object
        const vector3 = jsonData.vector3;
        const message = jsonData.message;

        console.log('Received Vector3:', vector3);
        console.log('Received message:', message);

        // Add the new data to the array
        allData.push({
            vector3,
            message
        });

        // Save the data to a JSON file
        const newJsonData = JSON.stringify(allData, null, 2);

        fs.writeFile(jsonFilePath, newJsonData, (err) => {
            if (err) {
                console.error('Error writing to JSON file:', err);
                return;
            }

            console.log('Data saved to JSON file:', newJsonData);
        });
    });

    socket.on('end', () => {
        console.log('client disconnected');
        socket.end();
    });
});

server.on('error', (err) => {
    console.error('Server error:', err);
});

server.listen(8124, () => {
    console.log('server bound');
});