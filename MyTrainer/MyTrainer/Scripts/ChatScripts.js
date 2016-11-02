$(function () {
    displayMessages();
    var chat = $.connection.chatHub;
    chat.client.broadcastMessage = function (name, message) {
        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(message).html();
        $('#discussion').append('<li><strong>' + encodedName
            + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
        var objDiv = document.getElementById("scrollBox");
        objDiv.scrollTop = objDiv.scrollHeight;
    };

    $('#displayname').val(name);
    $('#message').focus();
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            var text = $('#message').val();
            chat.server.send($('#displayname').val(), $('#message').val());
            $('#message').val('').focus();
            $.ajax({
                url: "../Users/saveMessages",
                type: "POST",
                data: { 'text': text },
                success: function (data) {
                    console.log(data);
                }
            });
        });
    });
});
function displayMessages() {
    var messages = '';
    $.ajax({
        url: "../Users/getMessages",
        type: "GET",
        success: function (data) {
            if (data != null) {
                console.log(data);
                $.each(data, function (key, value) {
                    messages += "<li><b>" + value.name + ":</b> " + value.messages + "</li>";

                })
                $('#discussion').html(messages);
                var objDiv = document.getElementById("scrollBox");
                objDiv.scrollTop = objDiv.scrollHeight;
            }
        }
    })
}