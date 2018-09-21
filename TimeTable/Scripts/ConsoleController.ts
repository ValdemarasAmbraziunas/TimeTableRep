class ConsoleController {
    private isVisible: boolean;
    private consoleButton: JQuery;
    private consoleContent: JQuery;
    private consoleLog: JQuery;

    constructor() {
        this.isVisible = false;
        this.consoleButton = $(".btn.console-button");
        this.consoleContent = $(".console-content");
        this.consoleLog = this.consoleContent.find('.log');
        this.bindEvents();
    }

    private bindEvents(): void {
        var getLog = (isFirst: boolean) => {
            var delay = isFirst ? 0 : 15001; 
            _.delay(() => {
                    $.get("/Lectures/getLog",
                        (response: any[]) => {
                            var messages: JQuery[] = response.map((message) => {
                                var listItem = $('<li></li>');
                                listItem.data('message-type', message.type);
                                var logEntry: JQuery = $('<div></div')
                                    .append(message.ID)
                                    .append(". ")
                                    .append(message.LogItem);
                                listItem.append(logEntry);
                                var undoButton: JQuery = $('<div>Undo</div>')
                                    .addClass('btn invisible undo')                                    
                                    .on('click', () => {
                                        $.post("Lectures/Undo",
                                            { logItemID: message.ID },
                                            () => {
                                                location.reload();
                                            });
                                    });
                                if (message.isUndoable){
                                listItem
                                    .hover(() => { undoButton.toggleClass('invisible'); })
                                        .append(undoButton);
                                }
                                return listItem;
                            });
                            this.consoleLog.empty().append(messages);
                            getLog(false);
                        });
                },
                delay);
        };
        this.consoleButton.on('click',
            () => {
                this.isVisible = !this.isVisible;
                this.consoleContent.toggleClass('hidden');
                getLog(true);
            });


    }
}