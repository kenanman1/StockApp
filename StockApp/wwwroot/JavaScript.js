let price = $("#price");
let currentPrice = null;
const maxRetries = 5;
let currentRetries = 0;

let symbol = $('#StockSymbol');
symbol.val(symbol.val().toUpperCase());

function connectWebSocket() {
    console.log("kkk");
    if (currentRetries > maxRetries) {
        console.log('Max retry attempts reached. Connection failed.');
        return;
    }

    const socket = new WebSocket('wss://ws.finnhub.io?token=cjh5fopr01qu5vpthpq0cjh5fopr01qu5vpthpqg');

    socket.addEventListener('open', function (event) {
        socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': symbol.val() }));
        currentRetries = 0;
    });

    socket.addEventListener('message', function (event) {
        if (event.data.type == "error") {
            console.log("error");
            price.textContent = event.data.msg;
            return;
        }

        var eventData = JSON.parse(event.data);
        if (eventData) {
            if (eventData.data) {
                console.log(eventData.data);
                var updatedPrice = JSON.parse(event.data).data[0].p;
                console.log(updatedPrice);
                currentPrice = updatedPrice;
                price.text(updatedPrice);
            }
        }
    });

    socket.addEventListener('error', (event) => {
        console.error('WebSocket error:', event);
        setTimeout(() => {
            currentRetries++;
            console.log(`Retrying connection (Attempt ${currentRetries})...`);
            connectWebSocket();
        }, 10000);
    });
}
connectWebSocket();


let element = $("#div-index")[0];
let classToRemove = "border-end";
let classToAdd = "border-bottom";

let mediaQuery = window.matchMedia("(max-width: 767px)");

function handleMediaQueryChange(mediaQuery) {
    if (mediaQuery.matches) {
        element.classList.remove(classToRemove);
        element.classList.add(classToAdd);
    } else {
        element.classList.add(classToRemove);
        element.classList.remove(classToAdd);
    }
}

mediaQuery.addListener(handleMediaQueryChange);

handleMediaQueryChange(mediaQuery);
