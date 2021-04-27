function changeImage(input) {
    var ext = input.files[0]['name'].substring(input.files[0]['name'].lastIndexOf('.') + 1).toLowerCase();
    if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#Image').attr('src', e.target.result);
            var formdata = new FormData();
            formdata.append("image", input.files[0]);
            $.ajax({
                contentType: false,
                processData: false,
                url: 'https://api.imgur.com/3/image',
                type: 'POST',
                headers: {
                    Authorization: 'Client-ID a5b6f56df93d2fb',
                    Accept: 'application/json'
                },
                mimeType: 'multipart/form-data',
                data: formdata,
                beforeSend: () => {
                    $('#loader').show();
                }
                ,
                complete: () => {
                    $('#loader').hide();
                },
                error: function (req, status, error) {
                    console.log("error: " + error);
                    console.log("req: " + req.data());
                    console.log("status: " + status);
                }
                ,
                success: function (result) {
                    result = JSON.parse(result);
                    var id = result.data.id;
                    $('#urlImg').val('https://imgur.com/' + id + '.jpg');
                    $('#Image').attr('src', 'https://imgur.com/' + id + '.jpg');
                }

            });
        }
        reader.readAsDataURL(input.files[0]);
    }
    else {
        $('#Image').attr('src', '/Image/jpg.png');
    }

}