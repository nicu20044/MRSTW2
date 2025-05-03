document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        document.querySelector(this.getAttribute('href')).scrollIntoView({
            behavior: 'smooth'
        });
    });
});

// Add hover effect for track cards
document.querySelectorAll('.track-card').forEach(card => {
    const playButton = card.querySelector('.play-button');

    card.addEventListener('mouseenter', () => {
        playButton.style.opacity = '1';
        playButton.style.transform = 'translateY(0)';
    });

    card.addEventListener('mouseleave', () => {
        playButton.style.opacity = '0';
        playButton.style.transform = 'translateY(8px)';
    });
});

// Play button click handler
document.querySelectorAll('.play-button').forEach(button => {
    button.addEventListener('click', (e) => {
        e.stopPropagation(); // Prevent card click event
        // Add your play functionality here
        console.log('Play clicked');
    });
});
