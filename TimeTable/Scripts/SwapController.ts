/**
 * Klasė atsakinga už paskaitų stumdymus
 */
class SwapController {

    private lecturesTimesQuery: string = ".lecture-time";
    private lectureQuery: string = ".lecture";

    private lecturesTimes: JQuery;
    private lectures: JQuery;

    private currentLectureId: string;
    private targetLectureId: string;
    private targetLectureTimeId: string;

    private swapLecturesTarget: string;
    private changeLectureTimeTarget: string;

    private isEditMode: boolean;
    private editModeButton: JQuery;
    private firstClick: boolean = true;
    private currentLectureIndicator = $("#currentLecture");
    private getChangesButton = $("#getChangesButton");
    private changeButton = $("#changeButton");
    private availableChangesTarget: string;

    constructor() {
        this.lecturesTimes = $(this.lecturesTimesQuery);
        this.lectures = $(this.lectureQuery);
        this.swapLecturesTarget = '/Lectures/Swap2Lectures';
        this.changeLectureTimeTarget = '/Lectures/ChangeLectureTime';
        this.availableChangesTarget = '/Lectures/GetAvailableChanges';
        this.isEditMode = false;
        this.editModeButton = $(".btn.editmode");
        this.bindEvents();
    }

    /**
     *Prikabina delegatus įvykiams
     */
    private bindEvents(): void {
        // Reguliuoja redagavimo režimą
        this.editModeButton.on('click',
            () => {
                $('.weekday-title, .lecture-time').toggleClass('editmode');
                this.isEditMode = !this.isEditMode;
                $('.changes-panel').toggleClass('hidden');
            });

        this.getChangesButton.on("click", () => {

            var changesOptions = {
                lectureToSwapID: this.currentLectureId,
                swapTeacher: ($("#swapLecturer")[0] as HTMLInputElement).checked,
                swapClassroom: ($("#swapRoom")[0] as HTMLInputElement).checked
            };
            $.post(this.availableChangesTarget,
                changesOptions,
                (response: any[]) => {
                    response.forEach((lecture, index) => {
                        var option: JQuery = $('<option></option>').attr('value', lecture).attr('data-id', lecture.ID).attr('data-time-id', lecture.TimeId)
                            .attr('data-day-id', lecture.DayId).text(lecture.Day + " " + lecture.Time + " " + lecture.SubjectN + " " + lecture.TeacherN);
                        $("#available-lectures").append(option);
                        this.changeButton.removeClass('hidden');
                    });
                    $("#available-lectures").removeClass('hidden');

                });
        });
        this.changeButton.on('click', () => {
            var selectedIndex = ($("#available-lectures")[0] as HTMLSelectElement).selectedIndex;
            var option = $($("#available-lectures option")[0]);
            console.log(option);
        });
        // Valdo paskaitu apkeitimą vietomis
        this.lecturesTimes.on('click',
            (event) => {
                if (!this.isEditMode) return;
                event.preventDefault();
                var targetLecture = $(event.target).closest(this.lectureQuery);
                var targetLectureTime = $(event.target).closest(this.lecturesTimesQuery);
                if (!targetLecture.hasClass('clicked')) {
                    targetLecture.addClass('clicked');
                    if (!this.currentLectureId) {
                        this.currentLectureId = targetLecture.data('id');
                        this.currentLectureIndicator.text(this.currentLectureId);
                    } else {
                        this.targetLectureId = targetLecture.data('id');
                        if (this.targetLectureId) {
                            $.post(this.swapLecturesTarget,
                                {
                                    "ID1": this.currentLectureId,
                                    "ID2": this.targetLectureId
                                },
                                (data) => { location.reload() });
                            return;
                        }
                        if (!this.firstClick) {
                            $.post(this.changeLectureTimeTarget,
                                {
                                    lectureID: this.currentLectureId,
                                    newLectureTimeId: targetLectureTime.data('id'),
                                    newLectureDayId: targetLectureTime.data('weekday-id')
                                },
                                (data) => { location.reload() });

                        }

                    }
                } else {
                    this.currentLectureId = null;
                    this.currentLectureIndicator.text("");
                    targetLecture.removeClass('clicked');
                }
                this.firstClick = !this.firstClick;
            });
    }
}