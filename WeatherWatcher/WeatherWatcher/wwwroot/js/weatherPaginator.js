$(document).ready(() => {
   
    $('.page-not-active-link').off().on('click', function (ev, picker) {
        let activeNumber = $("#page-active-link").html();
        let selectedNumber = $(this).html();
        let diff = selectedNumber - activeNumber;
        let currentOffset = JSON.parse(sessionStorage.getItem("currentOffset"));
       
        if (diff < 0) {
            currentOffset = currentOffset - (Math.abs(diff) * 50);
        } else {
            currentOffset = currentOffset + (diff * 50);
        }
        sessionStorage.setItem("currentOffset", currentOffset.toString());
        getPaginated();
    });
});
