$(".image-input").on({
    "change": function () {
        var file = $(this).prop("files")[0];
        var reader = new FileReader();

        reader.onloadend = function () {
            $(".image-label").css("background-image", 'url(' + reader.result + ')');
        }

        if (file) {
            reader.readAsDataURL(file);
        }
    }
});
