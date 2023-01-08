$(".add_img_inp").on({
    "change": function () {
        var file = $(this).prop("files")[0];
        var reader = new FileReader();

        reader.onloadend = function () {
            $(".add_img_lbl").css("background-image", 'url(' + reader.result + ')');
        }

        if (file) {
            reader.readAsDataURL(file);
        }
    }
});
