function loadExcel() {
    const files = $("#formFile")[0].files;
    if (!files || !files.length) {
        alert("choose files to upload");
        return;
    }
    let formData = new FormData();
    for (let i = 0; i < files.length; i++)
    {
        formData.append(files[i].name, files[i]);
    }
    $.ajax({
        type: "POST",
        url: window.origin + "/WeatherInfo/ArchiveLoading",
        data: formData,
        processData: false,
        contentType: false,
        error: function (data) {
            console.log(data.error);
        },
        success: () => {
            window.location.href = window.origin + "/WeatherInfo/LoadStatuses";
        }
    });

}

$(document).ready(() => {
    $("#loadWeatherExcel").on("click", () => {
        loadExcel();
    });
});