define([], function () {
    var paging = {
        setActive: setActive
    };

    return paging;

    /**
    * Set side navigation to active
    * @status: bool active or not
    * @element: the element to add active to
    */
    function setActive(status, element) {
        var element = document.getElementById(element);
        if (status) {
            element.className = "active";
        }
        else {
            element.className = " ";
        }
    }
});