function showRests() {
    $('.rest-interval').show();
}

function hideRests() {
    $('.rest-interval').hide();
}

function getIntervalLengthElements(intervalId) {
    let elements = $('tbody > [parent-interval-id=' + intervalId + ']');
    return elements;
}

const caretRightClassName = 'fas fa-caret-right';
const caretDownClassName = 'fas fa-caret-down';

function foldUnfoldInterval(sender) {
    let intervalId = sender.attributes['id'].value;
    let elements = getIntervalLengthElements(intervalId);
    if (sender.children[0].className === caretRightClassName) {
        $(elements).show();
        sender.children[0].className = caretDownClassName;
    }
    else {
        $(elements).hide();
        sender.children[0].className = caretRightClassName;
    }
}

function showIntervalLengths(intervalData, isVisible, modifyLengthsUrl, showIntervalData) {
    let intervalTr = $('tbody > [interval-id=' + intervalData.intervalId + ']');
    if (showIntervalData === true) {
        $(intervalTr).children('[field-name="Distance"]:first').text(intervalData.distanceFormatted);
        $(intervalTr).children('[field-name="Duration"]:first').text(intervalData.durationFormatted);
        $(intervalTr).children('[field-name="StartTime"]:first').text(intervalData.startTime);
        $(intervalTr).children('[field-name="StrokeCount"]:first').text(intervalData.strokeCountFormatted);
        $(intervalTr).children('[field-name="Pace"]:first').text(intervalData.paceFormatted);
        $(intervalTr).children('[field-name="Swolf"]:first').text(intervalData.swolfFormatted);
    }

    let oldLengthElements = getIntervalLengthElements(intervalData.intervalId);
    $(oldLengthElements).remove();
    for (i = intervalData.lengths.length - 1; i >= 0; --i) {
        let lengthElement = createLengthElement(intervalData.lengths[i], intervalData.intervalId, isVisible, modifyLengthsUrl);
        $(intervalTr).after(lengthElement);
    }
}

function createLengthElement(lengthData, intervalId, isVisible, modifyLengthsUrl) {
    let visibleAttr = '';
    if (isVisible === false) {
        visibleAttr = ' style="display: none;"';
    }
    let elemenText =
        `<tr class="bg-secondary" parent-interval-id="${intervalId}" stroke-type="${lengthData.strokeTypeId}"${visibleAttr}>` +
            `<td class="pl-2" length-no="${lengthData.lengthNo}"><span style="margin-left:10px;">&nbsp;</span>${lengthData.lengthNo}</td>` +
            `<td field-name="Duration">${lengthData.durationFormatted}</td>` +
            '<td></td>' +
            `<td>${lengthData.paceFormatted}</td>` +
            `<td field-name="Distance">${lengthData.distanceFormatted}</td>` +
            `<td field-name="StrokeCount">${lengthData.strokeCount}</td>` +
            '<td></td>' +
            `<td>${lengthData.strokeTypeName}</td>` +
            `<td><a href="#" onclick="joinLengthWithNext(this, '${modifyLengthsUrl}'); return false;"><i class="fas fa-arrows-alt-v"></i></a></td>` +
        '</tr>';
    return elemenText;
}

function getIntervalDisplayData(intervalElement, lengthNodes) {
    let intervalId = $(intervalElement).attr('interval-id');
    let intervalTypeId = $(intervalElement).attr('interval-type-id');
    let intervalNo = $(intervalElement).attr('interval-no');
    let distance = $(intervalElement).children('[field-name="Distance"]:first').text();
    let duration = $(intervalElement).children('[field-name="Duration"]:first').text();
    let startTime = $(intervalElement).children('[field-name="StartTime"]:first').text();
    let strokeCount = $(intervalElement).children('[field-name="StrokeCount"]:first').text();
    let intervalTypeName = $(intervalElement).children('[field-name="IntervalTypeName"]:first').text();

    let lengthCount = lengthNodes.length;
    let lengths = [];
    for (var i = 0; i < lengthCount; ++i) {
        let lengthTr = lengthNodes[i];
        lengths.push(getIntervalLengthDisplayData(lengthTr));
    }

    let result = {
        "intervalId": parseInt(intervalId),
        "intervalNo": parseInt(intervalNo),
        "durationFormatted": duration,
        "distanceFormatted": distance,
        "strokeCountFormatted": strokeCount,
        "startTime": startTime,
        "intervalTypeId": intervalTypeId,
        "intervalTypeName": intervalTypeName,
        "lengths": lengths
    };
    return result;
}

function joinLengthWithNext(sender, modifyLengthsUrl) {
    let parentTr = sender.closest('tr');
    let intervalId = $(parentTr).attr('parent-interval-id');
    let lengthNo = $(parentTr).find('td:first').attr('length-no');
    let intervalTr = $('tbody >[interval-id=' + intervalId + ']');
    let lengthNodes = $('tbody >[parent-interval-id=' + intervalId + ']');
    let intervalData = getIntervalDisplayData(intervalTr, lengthNodes);

    console.log('intervalId: ' + intervalId + ', lengthNo: ' + lengthNo);
    $.ajax({
        type: "POST",
        url: modifyLengthsUrl,
        contentType: "application/json",
        data: JSON.stringify({ "mode": 0, "selectedLengthNo": parseInt(lengthNo), "intervalData": intervalData }),
        dataType: "json",
        //processData: false,
        success: function (data) {
            console.info('Result: ' + data.success + ', message: ' + data.message);
            showIntervalLengths(data.result, true, modifyLengthsUrl, true);
        },
        error: function (data) {
            console.warn(data.responseText);
        }
    });
}

function getIntervalLengthDisplayData(lengthNode) {
    let result = {
        "lengthNo": parseInt($(lengthNode).children(':first').attr('length-no')),
        "durationFormatted": $(lengthNode).children('[field-name="Duration"]:first').text(),
        "distanceFormatted": parseInt($(lengthNode).children('[field-name="Distance"]:first').text().replace(' m', '')),
        "strokeCount": parseInt($(lengthNode).children('[field-name="StrokeCount"]:first').text()),
        "strokeTypeId": parseInt($(lengthNode).attr('stroke-type'))
    };
    return result;
}

var intervalDataJson = "";

//$(function () {
//    console.log('setting onclick');
//    $('#showRestsOff').on('click', showHideRests());
//    $('#showRestsOn').click(showHideRests());
//    console.log('onclick set');
//})
