var express  = require('express');
var app      = express();
var bodyParser = require('body-parser');

app.use(express.static(__dirname + '/'));
app.use(bodyParser.urlencoded({'extended':'true'}));
app.use(bodyParser.json());
app.use(bodyParser.json({ type: 'application/vnd.api+json' }));
    
app.listen(9001);
console.log("Todo app started on port 9001");