var SwapController = (function () {
    function SwapController() {
        this.lecturesTimesQuery = ".lecture-time";
        this.lectureQuery = ".lecture";
        this.firstClick = true;
        this.currentLectureIndicator = $("#currentLecture");
        this.getChangesButton = $("#getChangesButton");
        this.changeButton = $("#changeButton");
        this.lecturesTimes = $(this.lecturesTimesQuery);
        this.lectures = $(this.lectureQuery);
        this.swapLecturesTarget = '/Lectures/Swap2Lectures';
        this.changeLectureTimeTarget = '/Lectures/ChangeLectureTime';
        this.availableChangesTarget = '/Lectures/GetAvailableChanges';
        this.isEditMode = false;
        this.editModeButton = $(".btn.editmode");
        this.bindEvents();
    }
    SwapController.prototype.bindEvents = function () {
        var _this = this;
        this.editModeButton.on('click', function () {
            $('.weekday-title, .lecture-time').toggleClass('editmode');
            _this.isEditMode = !_this.isEditMode;
            $('.changes-panel').toggleClass('hidden');
        });
        this.getChangesButton.on("click", function () {
            var changesOptions = {
                lectureToSwapID: _this.currentLectureId,
                swapTeacher: $("#swapLecturer")[0].checked,
                swapClassroom: $("#swapRoom")[0].checked
            };
            $.post(_this.availableChangesTarget, changesOptions, function (response) {
                response.forEach(function (lecture, index) {
                    var option = $('<option></option>').attr('value', lecture).attr('data-id', lecture.ID).attr('data-time-id', lecture.TimeId)
                        .attr('data-day-id', lecture.DayId).text(lecture.Day + " " + lecture.Time + " " + lecture.SubjectN + " " + lecture.TeacherN);
                    $("#available-lectures").append(option);
                    _this.changeButton.removeClass('hidden');
                });
                $("#available-lectures").removeClass('hidden');
            });
        });
        this.changeButton.on('click', function () {
            var selectedIndex = $("#available-lectures")[0].selectedIndex;
            var option = $($("#available-lectures option")[0]);
            console.log(option);
        });
        this.lecturesTimes.on('click', function (event) {
            if (!_this.isEditMode)
                return;
            event.preventDefault();
            var targetLecture = $(event.target).closest(_this.lectureQuery);
            var targetLectureTime = $(event.target).closest(_this.lecturesTimesQuery);
            if (!targetLecture.hasClass('clicked')) {
                targetLecture.addClass('clicked');
                if (!_this.currentLectureId) {
                    _this.currentLectureId = targetLecture.data('id');
                    _this.currentLectureIndicator.text(_this.currentLectureId);
                }
                else {
                    _this.targetLectureId = targetLecture.data('id');
                    if (_this.targetLectureId) {
                        $.post(_this.swapLecturesTarget, {
                            "ID1": _this.currentLectureId,
                            "ID2": _this.targetLectureId
                        }, function (data) { location.reload(); });
                        return;
                    }
                    if (!_this.firstClick) {
                        $.post(_this.changeLectureTimeTarget, {
                            lectureID: _this.currentLectureId,
                            newLectureTimeId: targetLectureTime.data('id'),
                            newLectureDayId: targetLectureTime.data('weekday-id')
                        }, function (data) { location.reload(); });
                    }
                }
            }
            else {
                _this.currentLectureId = null;
                _this.currentLectureIndicator.text("");
                targetLecture.removeClass('clicked');
            }
            _this.firstClick = !_this.firstClick;
        });
    };
    return SwapController;
}());
