window.stompClient = {
    client: null,

    connect: function (url, headers, onConnected, onError,token) {
        var socket = new SockJS(url);
        this.client = Stomp.over(socket);
        this.client.connect({ Authorization: `Bearer ${token}` }, function (frame) {
            onConnected.invokeMethodAsync("Callback","frame");
        }, function (error) {
            onError(error);
        });
    },

    disconnect: function () {
        if (this.client !== null) {
            this.client.disconnect();
        }
    },

    subscribe: function (destination, callback) {
        if (this.client !== null) {
            return this.client.subscribe(destination, function (message) {
                callback.invokeMethodAsync("Callback",message.body);
            });
        }
    },

    send: function (destination, headers, body) {
        if (this.client !== null) {
            this.client.send(destination, headers, body);
        }
    }
};
