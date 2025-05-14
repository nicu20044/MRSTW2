
    document.addEventListener('DOMContentLoaded', function() {
    // Get selected plan from localStorage
    const selectedPlan = JSON.parse(localStorage.getItem('selectedPlan'));
    if (selectedPlan) {
    document.getElementById('selectedPlanName').textContent = selectedPlan.name;
    document.getElementById('selectedPlanPrice').textContent = `$${selectedPlan.price}/month`;
}

    // Card number formatting
    document.getElementById('card-number').addEventListener('input', function(e) {
    let value = e.target.value.replace(/\D/g, '');
    let formattedValue = value.match(/.{1,4}/g)?.join(' ') || '';
    e.target.value = formattedValue;
});

    // Expiry date formatting
    document.getElementById('exp-date').addEventListener('input', function(e) {
    let value = e.target.value.replace(/\D/g, '');
    if (value.length >= 2) {
    value = value.slice(0,2) + '/' + value.slice(2);
}
    e.target.value = value;
});

    // CVV number only
    document.getElementById('cvv').addEventListener('input', function(e) {
    e.target.value = e.target.value.replace(/\D/g, '');
});
});
