// JavaScript source code

var ws;
var Init = function (ip, port, receiveMsg) {
    ws = new WebSocket('ws://' + ip + ':' + port);
    ws.onopen = function () {
        HeartCheck.start();
        ws.send('W_SETID@Xzy@' + guid + '@XEnd@');
        alert("连接成功！");
    }
    ws.onmessage = function (msg) {
        HeartCheck.reset();
        var msgStr= msg.data;
        receiveMsg(msgStr.substring(0, msgStr.length - 6));
    }
    ws.onerror = function (err) {
        alert(JSON.stringify(err));
    }
    ws.onclose = function () {

    }
}

var SendMessage = function (context) {
    ws.send('MSG@Xzy@' + context +'@XEnd@');
}

var HeartCheck = {
    timeout: 60000, //60ms
    timeoutObj: null,
    serverTimeoutObj: null,
    reset: function () {
        clearTimeout(this.timeoutObj);
        clearTimeout(this.serverTimeoutObj);
        this.start();
    },
    start: function () {
        var self = this;
        this.timeoutObj = setTimeout(function () {
            try {
                ws.send("HeartBeat");
            } catch (e) {
                ws.onopen();
            }
        }, this.timeout)
    },
}