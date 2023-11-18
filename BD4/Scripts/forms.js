const dateOrderField = document.getElementById('order_date');
const datePayField = document.getElementById('pay_date');
const dateShipField = document.getElementById('ship_date');

const aspDateOrderField = document.getElementById('date_order');
const aspDatePayField = document.getElementById('date_pay');
const aspDateShipField = document.getElementById('date_ship')

if (aspDateOrderField.value !== '') {
    dateOrderField.value = aspDateOrderField.value;
}

if (aspDatePayField.value !== '') {
    datePayField.value = aspDatePayField.value;
}

if (aspDateShipField.value !== '') {
    dateShipField.value = aspDateShipField.value;
}

dateOrderField.addEventListener("input", (event) => {

    console.log(event.target.value === '');
    aspDateOrderField.value = event.target.value;
})

datePayField.addEventListener("input", (event) => {
    aspDatePayField.value = event.target.value
})

dateShipField.addEventListener("input", (event) => {
    aspDateShipField.value = event.target.value
})