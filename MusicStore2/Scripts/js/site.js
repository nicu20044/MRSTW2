document.addEventListener('DOMContentLoaded', function () {
    initializeTrackCardEvents();

    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth'
                });
            }
        });
    });

    const viewAllBtn = document.getElementById('viewAllBtn');
    if (viewAllBtn) {
        viewAllBtn.addEventListener('click', function () {
            fetch('/Home/LoadAllNewReleases')
                .then(response => {
                    if (!response.ok) {
                        throw new Error("Network response was not ok");
                    }
                    return response.text();
                })
                .then(html => {
                    const container = document.getElementById('newReleasesContainer');
                    if (container) {
                        container.innerHTML = html;
                        initializeTrackCardEvents(); 
                    }
                })
                .catch(error => {
                    console.error('Eroare la încărcarea produselor:', error);
                });
        });
    }
});

function initializeTrackCardEvents() {
    document.querySelectorAll('.track-card').forEach(card => {
        const playButton = card.querySelector('.play-button');
        if (!playButton) return;

        card.addEventListener('mouseenter', () => {
            playButton.style.opacity = '1';
            playButton.style.transform = 'translateY(0)';
        });

        card.addEventListener('mouseleave', () => {
            playButton.style.opacity = '0';
            playButton.style.transform = 'translateY(8px)';
        });
    });

    // Eveniment click pe butonul play
    document.querySelectorAll('.play-button').forEach(button => {
        button.addEventListener('click', (e) => {
            e.stopPropagation();
            // Aici adaugă logica pentru redare audio dacă ai un player
            console.log('Play clicked');
        });
    });
}

const allAudioElements = document.querySelectorAll("audio");

function togglePlay(button) {
    const audioId = button.getAttribute("data-audio-id");
    const audio = document.getElementById(audioId);

    allAudioElements.forEach(a => {
        if (a !== audio) {
            a.pause();
            a.currentTime = 0;
            updateButtonIcon(a.id, false);
        }
    });

    if (audio.paused) {
        audio.play();
        updateButtonIcon(audioId, true);
    } else {
        audio.pause();
        updateButtonIcon(audioId, false);
    }
}

function updateButtonIcon(audioId, isPlaying) {
    const button = document.querySelector(`.play-toggle[data-audio-id="${audioId}"]`);
    const svg = button.querySelector("svg");

    if (isPlaying) {
        svg.innerHTML = `<path d="M6 4h4v16H6zm8 0h4v16h-4z" fill="currentColor"/>`;
    } else {
        svg.innerHTML = `<path d="M8 5v14l11-7z" fill="currentColor"/>`;
    }
}
document.getElementById("viewAllBtn").addEventListener("click", function () {
    fetch('/Home/LoadAllNewReleases')
        .then(response => response.text())
        .then(html => {
            document.getElementById("new-releases-container").innerHTML = html;
            document.getElementById("viewAllBtn").style.display = "none";
        });
});

