window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            "Success!",
            message,
            "success"
        );
    }
    if (type === "error") {
        Swal.fire(
            "Something went wrong",
            message,
            "error"
        );
    }
}

//window.PlaySound = function() {
//    document.getElementById('sound').play();
//}

//window.PlaySound = function() {
//    document.getElementById('WhoIsIt').play();
//}

//window.PlaySound = function() {
//    document.getElementById('GameOver').play();
//}

window.PlayAudioFile = (src) => {
    var audio = document.getElementById('player');
    if (audio != null) {
        var audioSource = document.getElementById('playerSource');
        if (audioSource != null) {
            audioSource.src = src;
            audio.load();
            audio.play();
        }
    }
}