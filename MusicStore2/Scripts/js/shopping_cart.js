
    document.addEventListener('DOMContentLoaded', function() {
    const planButtons = document.querySelectorAll('.choose-plan-btn');

    planButtons.forEach(button => {
    button.addEventListener('click', function() {
    const planName = this.closest('.plan-card').querySelector('.plan-name').textContent;
    console.log(`Selected plan: ${planName}`);
    // Add your plan selection logic here
});
});
});
    document.addEventListener('DOMContentLoaded', function() {
    const planButtons = document.querySelectorAll('.choose-plan-btn');

    planButtons.forEach(button => {
    button.addEventListener('click', function() {
    const planCard = this.closest('.plan-card');
    const planName = planCard.querySelector('.plan-name').textContent;
    const planPrice = planCard.querySelector('.plan-price').textContent.match(/\d+\.\d+/)[0];

    // Store the selected plan details in localStorage
    localStorage.setItem('selectedPlan', JSON.stringify({
    name: planName,
    price: planPrice
}));

    // Redirect to payment page
    window.location.href = 'PaymentPage';
});
});
});
