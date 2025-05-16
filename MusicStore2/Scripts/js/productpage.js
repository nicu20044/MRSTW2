document.addEventListener("DOMContentLoaded", function() {
    
    var waveformContainer = document.getElementById('waveform');
    if (!waveformContainer) {
        console.error('Containerul #waveform nu a fost găsit!');
        return;
    }
    var wavesurfer = WaveSurfer.create({
        container: '#waveform',
        waveColor: 'white',
        progressColor: 'blue',
        cursorColor: 'red',
        barWidth: 2,
        barRadius: 3,
        height: 50,
        responsive: true
    });

    var playBtn = document.getElementById("playBtn");
    var stopBtn = document.getElementById("stopBtn");
    var volumeBtn = document.getElementById("volumeBtn");
    var volumeSlider = document.getElementById("volumeSlider");

    wavesurfer.load('/UploadedAudios/black.mp3');

    playBtn.onclick = function() {
        wavesurfer.playPause();
        playBtn.src = playBtn.src.includes("play.png") ? "/Scripts/images/pause.png" : "/Scripts/images/play.png";
    }

    stopBtn.onclick = function() {
        wavesurfer.stop();
        playBtn.src = "/Scripts/images/play.png";
    };

    volumeBtn.onclick = function() {
        wavesurfer.toggleMute();
        volumeBtn.src = volumeBtn.src.includes("volume.png") ? "/Scripts/images/mute.png" : "/Scripts/images/volume.png";
    };

    volumeSlider.addEventListener('input', function() {
        wavesurfer.setVolume(parseFloat(this.value));
    });

    wavesurfer.setVolume(1);

    wavesurfer.on('finish', function() {
        playBtn.src = "/Scripts/images/play.png";
        wavesurfer.stop();
    });

    wavesurfer.on('error', function(error) {
        console.error('Eroare la încărcarea fișierului MP3:', error);
        alert('Nu s-a putut încărca fișierul audio. Verificați calea sau permisiunile.');
    });

    wavesurfer.on('ready', function() {
        console.log('Waveform generat cu succes!');
    });

    var licenseButtons = document.querySelectorAll(".license");
    
       
        licenseButtons.forEach(function(button) {
            button.addEventListener("click", function() {
                licenseButtons.forEach(btn => btn.classList.remove("selected"));
                this.classList.add("selected");
                var selectedPrice = this.textContent.match(/\$\d+(\.\d{2})?/g);
                document.querySelector(".total-price").textContent = selectedPrice ? selectedPrice[0] : "Negotiate Price";
            });
        });
});