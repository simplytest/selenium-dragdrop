function createDragEvent(eventName, options) {
    var event = document.createEvent('HTMLEvents');
    event.initEvent('DragEvent', true, true);
    //" var event = document.createEvent("DragEvent");
    var screenX = window.screenX + options.clientX;
    var screenY = window.screenY + options.clientY;
    var clientX = options.clientX;
    var clientY = options.clientY;
    var dataTransfer = {
        data: options.dragData == null ? {} : options.dragData,
        setData: function (eventName, val) {
            if (typeof val === 'string') {
                this.data[eventName] = val;
            }
        },
        getData: function (eventName) {
            return this.data[eventName];
        },
        clearData: function () {
            return this.data = {};
        },
        setDragImage: function (dragElement, x, y) { }
    };
    var eventInitialized = false;
    if (event != null && event.initDragEvent) {
        try {
            event.initDragEvent(eventName, true, true, window, 0, screenX, screenY, clientX, clientY, false, false, false, false, 0, null, dataTransfer);
            event.initialized = true;
        } catch (err) {
            // no-op
        }
    }
    if (!eventInitialized) {
        event = document.createEvent("CustomEvent");
        event.initCustomEvent(eventName, true, true, null);
        event.view = window;
        event.detail = 0;
        event.screenX = screenX;
        event.screenY = screenY;
        event.clientX = clientX;
        event.clientY = clientY;
        event.ctrlKey = false;
        event.altKey = false;
        event.shiftKey = false;
        event.metaKey = false;
        event.button = 0;
        event.relatedTarget = null;
        event.dataTransfer = dataTransfer;
    }
    return event;
}

/* Creates a mouse event */
function createMouseEvent(eventName, options) {
    var event = document.createEvent("MouseEvent");
    var screenX = window.screenX + options.clientX;
    var screenY = window.screenY + options.clientY;
    var clientX = options.clientX;
    var clientY = options.clientY;
    if (event != null && event.initMouseEvent) {
        event.initMouseEvent(eventName, true, true, window, 0, screenX, screenY, clientX, clientY, false, false, false, false, 0, null);
    } else {
        event = document.createEvent("CustomEvent");
        event.initCustomEvent(eventName, true, true, null);
        event.view = window;
        event.detail = 0;
        event.screenX = screenX;
        event.screenY = screenY;
        event.clientX = clientX;
        event.clientY = clientY;
        event.ctrlKey = false;
        event.altKey = false;
        event.shiftKey = false;
        event.metaKey = false;
        event.button = 0;
        event.relatedTarget = null;
    }
    return event;
}

/* Runs the events */
function dispatchEvent(webElement, eventName, event) {
    if (webElement.dispatchEvent) {
        webElement.dispatchEvent(event);
    } else if (webElement.fireEvent) {
        webElement.fireEvent("on" + eventName, event);
    }
}

/* Simulates an individual event */
function simulateEventCall(element, eventName, dragStartEvent, options) {
    var event = null;
    if (eventName.indexOf("mouse") > -1) {
        event = createMouseEvent(eventName, options);
    } else {
        event = createDragEvent(eventName, options);
    }
    if (dragStartEvent != null) {
        event.dataTransfer = dragStartEvent.dataTransfer;
    }
    dispatchEvent(element, eventName, event);
    return event;
}


function simulateEvent(element, eventName, clientX, clientY, dragData) {
    return simulateEventCall(element, eventName, null, { clientX: clientX, clientY: clientY, dragData: dragData });
}

//var event = simulateEvent(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4]);
//if (event.dataTransfer != null) {
//    return event.dataTransfer.data;
//}


function simulateHTML5DragAndDrop(dragFrom, dragTo, dragFromX, dragFromY, dragToX, dragToY) {
    var mouseDownEvent = simulateEventCall(dragFrom, "mousedown", null, { clientX: dragFromX, clientY: dragFromY });
    var dragStartEvent = simulateEventCall(dragFrom, "dragstart", null, { clientX: dragFromX, clientY: dragFromY });
    var dragEnterEvent = simulateEventCall(dragTo, "dragenter", dragStartEvent, { clientX: dragToX, clientY: dragToY });
    var dragOverEvent = simulateEventCall(dragTo, "dragover", dragStartEvent, { clientX: dragToX, clientY: dragToY });
    var dropEvent = simulateEventCall(dragTo, "drop", dragStartEvent, { clientX: dragToX, clientY: dragToY });
    var dragEndEvent = simulateEventCall(dragFrom, "dragend", dragStartEvent, { clientX: dragToX, clientY: dragToY });
}
