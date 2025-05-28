document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('paymentForm').addEventListener('submit', function (event) {
        event.preventDefault();

        const cardNumber = document.getElementById('card-number').value;
        const cardHolder = document.getElementById('card-holder').value;
        const expDate = document.getElementById('exp-date').value;
        const cvv = document.getElementById('cvv').value;

        if (cardNumber && cardHolder && expDate && cvv) {
            const songFileNames = Array.from(document.querySelectorAll('input[name="song-file-name"]'))
                .map(input => input.value)
                .filter(fileName => fileName);

            if (songFileNames.length > 0) {
                console.log('Attempting to download songs:', songFileNames);
                alert('Payment successful! Songs will now start downloading.');

                songFileNames.forEach((fileName, index) => {
                    setTimeout(() => {
                        const link = document.createElement('a');
                        link.href = '/ShoppingCart/DownloadSong?fileName=' + encodeURIComponent(fileName);
                        link.download = fileName;
                        document.body.appendChild(link);
                        link.click();
                        document.body.removeChild(link);
                        console.log(`Triggered download for: ${fileName}`);
                    }, index * 1000);
                });

            } else {
                console.log('No song file names found.');
                alert('No songs available for download. Check debug info.');
            }
        } else {
            alert('Please fill all fields correctly.');
        }
    });
});
