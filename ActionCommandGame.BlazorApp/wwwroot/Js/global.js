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
            "It didn't work.",
            message,
            "error"
        );
    }
}


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