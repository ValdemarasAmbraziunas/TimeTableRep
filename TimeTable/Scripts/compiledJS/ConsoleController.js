var ConsoleController = (function () {
    function ConsoleController() {
        this.isVisible = false;
        this.consoleButton = $(".btn.console-button");
        this.consoleContent = $(".console-content");
        this.consoleLog = this.consoleContent.find('.log');
        this.bindEvents();
    }
    ConsoleController.prototype.bindEvents = function () {
        var _this = this;
        var getLog = function (isFirst) {
            var delay = isFirst ? 0 : 15001;
            _.delay(function () {
                $.get("/Lectures/getLog", function (response) {
                    var messages = response.map(function (message) {
                        var listItem = $('<li></li>');
                        listItem.data('message-type', message.type);
                        var logEntry = $('<div></div')
                            .append(message.ID)
                            .append(". ")
                            .append(message.LogItem);
                        listItem.append(logEntry);
                        var undoButton = $('<div>Undo</div>')
                            .addClass('btn invisible undo')
                            .on('click', function () {
                            $.post("Lectures/Undo", { logItemID: message.ID }, function () {
                                location.reload();
                            });
                        });
                        if (message.isUndoable) {
                            listItem
                                .hover(function () { undoButton.toggleClass('invisible'); })
                                .append(undoButton);
                        }
                        return listItem;
                    });
                    _this.consoleLog.empty().append(messages);
                    getLog(false);
                });
            }, delay);
        };
        this.consoleButton.on('click', function () {
            _this.isVisible = !_this.isVisible;
            _this.consoleContent.toggleClass('hidden');
            getLog(true);
        });
    };
    return ConsoleController;
}());
