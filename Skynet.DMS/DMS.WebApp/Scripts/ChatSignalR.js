var tryingToReconnect = false;


$(function () {
    var chat = $.connection.chatHub;
    $.connection.hub.qs = { "version": "1.0" };
    $.connection.hub.logging = true;
    $.connection.hub.reconnecting(function () {
        tryingToReconnect = true;
    });

    $.connection.hub.reconnected(function () {
        tryingToReconnect = false;
    });

    $.connection.hub.disconnected(function () {
        if ($.connection.hub.lastError) {
             alert("Disconnected. Reason: " + $.connection.hub.lastError.message);
        }
        if (tryingToReconnect) {
            // notifyUserOfDisconnect(); // Your function to notify user.
            setTimeout(function () {
                $.connection.hub.start();
            }, 5000); // Restart connection after 5 seconds.
        }
    });

    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page. 
        $('#discussion').append('<li><strong>' + htmlEncode(name)
            + '</strong>: ' + htmlEncode(message) + '</li>');
    };
    chat.client.stopClient = function () {
        $.connection.hub.stop();
    }
    // Get the user name and store it to prepend to messages.
    $('#displayname').val(prompt('Enter your name:', ''));
    // Set initial focus to message input box.  
    $('#message').focus();
    // Start the connection.
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            // Call the Send method on the hub. 
            chat.server.send($('#displayname').val(), $('#message').val());
            // Clear text box and reset focus for next comment. 
            $('#message').val('').focus();
        });
    });
});
// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
