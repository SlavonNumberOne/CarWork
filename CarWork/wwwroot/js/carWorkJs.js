
const currentRefuielingIds = [];
const currentDriverIds = [];
const currentDeliveryOrderIds = [];

const mapElement = document.getElementById("map");

const addDriver = (driver) => {
    const mapCarElement = document.createElement("img");
    mapCarElement.classList.add("car");
    mapCarElement.id = "d-" + driver.id;
    mapCarElement.src = "/img/car1.webp";
    mapCarElement.style.left = (driver.car.location * 0.5) + "%";
    if (driver.operation.direction == 0) {
        mapCarElement.classList.add("car-left");
    }
    else {
        mapCarElement.classList.add("car-right");
    }

    currentDriverIds.push(driver.id);

    mapElement.appendChild(mapCarElement);
    infoElement(driver);
}

const addFuel = (refuieling) => {
    const fuelElement = document.createElement("div");
    fuelElement.classList.add("fuel");
    fuelElement.id = "f-" + refuieling.id;
    fuelElement.style.left = (refuieling.location * 0.5) + "%";
    fuelElement.innerHTML = refuieling.name;
    const ramdomValue = Math.random();
    if (ramdomValue > 0.5) {
        fuelElement.style.top = "20px";
    }
    else {
        fuelElement.style.top = "350px";
    } 
    currentRefuielingIds.push(refuieling.id);
    mapElement.appendChild(fuelElement);
}

const addOrder = (deliveryOrder) => {
    const clientElement = document.createElement("img");
    clientElement.classList.add("child");
    clientElement.id = "c-" + deliveryOrder.id;
    clientElement.src = "/img/05.png";
    clientElement.style.left = (deliveryOrder.passenger.location * 0.5) + "%";

    const deliveryElement = document.createElement("img");
    deliveryElement.classList.add("delivery");
    deliveryElement.id = "d-" + deliveryOrder.id;
    deliveryElement.src = "/img/deliveryOrder.png";
    deliveryElement.style.left = (deliveryOrder.deliveryLocation * 0.5) + "%";

    const ramdomValue = Math.random();
    if (ramdomValue > 0.5) {
        clientElement.style.top = "61px";
        deliveryElement.style.top = "65px";
    }
    else {
        clientElement.style.top = "330px";
        deliveryElement.style.top = "330px";
    }

    currentDeliveryOrderIds.push(deliveryOrder.id);
    mapElement.appendChild(clientElement);
    mapElement.appendChild(deliveryElement);
}

const deleteDriver = (id) => {
    const indexToDelete = currentDriverIds.indexOf(id);
    if (indexToDelete !== -1) {
        currentDriverIds.splice(indexToDelete, 1);
    }

    document.getElementById("d-" + id).remove();

}

const deleteFuel = (id) => {
    const indexToDelete = currentRefuielingIds.indexOf(id);
    if (indexToDelete !== -1) {
        currentRefuielingIds.splice(indexToDelete, 1);
    }
    document.getElementById("f-" + id).remove();
}

const deleteOrder = (id) => {
    const indexToDelete = currentDeliveryOrderIds.indexOf(id);
    if (indexToDelete !== -1) {
        currentDeliveryOrderIds.splice(indexToDelete, 1);
    }

    document.getElementById("c-" + id).remove();
    document.getElementById("d-" + id).remove();
}

const updateDriver = (driver) => {
    var driverElement = document.getElementById("d-" + driver.id);
    driverElement.style.left = (driver.car.location * 0.5) + "%";
    if (driver.operation.direction == 0) {
        driverElement.classList.add("car-left");
        driverElement.classList.remove("car-right");

    }
    else {
        driverElement.classList.add("car-right");
        driverElement.classList.remove("car-left");

    }
}

const updateFuel = (refuieling) => {

}

const updateOrder = (deliveryOrder) => {
    if (deliveryOrder.statusOrder < 3) {
        document.getElementById("c-" + deliveryOrder.id).style.display = "none";
    }
}

const updateMapObjects = (mapObjectsInfoMessage) => {
    updateMapDrivers(mapObjectsInfoMessage.drivers);
    updateMapFuels(mapObjectsInfoMessage.refuielings);
    updateMapOrder(mapObjectsInfoMessage.deliveryOrders);
}

const updateMapDrivers = (drivers) => {
    const driversToUpdate = drivers.filter(d => currentDriverIds.includes(d.id));
    const driversToAdd = drivers.filter(d => !currentDriverIds.includes(d.id));

    const newDriverIds = drivers.map(d => d.id);
    const driverIdsToDelete = currentDriverIds.filter(id => !newDriverIds.includes(id));

    for (let driver of driversToUpdate) {
        updateDriver(driver);
        updateInfoDriver(driver);
    }
    for (let driver of driversToAdd) {
        addDriver(driver);
    }
    for (let driverId of driverIdsToDelete) {
        deleteDriver(driverId);
    }

    
}

const updateMapFuels = (refuielings) => {
    const fuelsToUpdate = refuielings.filter(f => currentRefuielingIds.includes(f.id));
    const fuelsToAdd = refuielings.filter(f => !currentRefuielingIds.includes(f.id));

    const newFuelIds = refuielings.map(f => f.id);
    const fuelIdsToDelete = currentRefuielingIds.filter(id => !newFuelIds.includes(id));

    for (let refuieling of fuelsToUpdate) {
        updateFuel(refuieling);
    }
    for (let refuieling of fuelsToAdd) {
        addFuel(refuieling);
    }
    for (let refuielingId of fuelIdsToDelete) {
        deleteFuel(refuielingId);
    }
}

const updateMapOrder = (deliveryOrders) => {
    const ordersToUpdate = deliveryOrders.filter(d => currentDeliveryOrderIds.includes(d.id));
    const ordersToAdd = deliveryOrders.filter(d => !currentDeliveryOrderIds.includes(d.id));

    const newOrderIds = deliveryOrders.map(d => d.id);
    const orderIdsToDelete = currentDeliveryOrderIds.filter(id => !newOrderIds.includes(id));

    for (let deliveryOrder of ordersToUpdate) {
        updateOrder(deliveryOrder);
    }
    for (let deliveryOrder of ordersToAdd) {
        addOrder(deliveryOrder);
    }
    for (let deliveryOrderId of orderIdsToDelete) {
        deleteOrder(deliveryOrderId);
    }
}



const signalRConnection = new signalR.HubConnectionBuilder()
    .withUrl(`/carworkhub`)
    .configureLogging(signalR.LogLevel.Information)
    .build();
signalRConnection.on("ListOfCarsWasUpdated",
    (mapObjectsInfoMessage) => {
        updateMapObjects(mapObjectsInfoMessage);
    });

const startSignalRConnection = async () => {
    try {
        await signalRConnection.start();
        console.log("SignalR Connected");
    } catch (error) {
        console.log(`Signal Error: ${error}`);
        setTimeout(startSignalRConnection, 5000);
    }
};

startSignalRConnection();

function newOrderClick() {
    fetch("api/orders/create", {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'cors', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json'
        }
    });
}

const infoElement = (driver) => {
    const infoMessegeElement = document.getElementById("b");
    
    const driverElement = document.createElement("div");
    driverElement.classList.add("new");

    const nameHeaderElement = document.createElement("div");
    nameHeaderElement.classList.add( "driver-name");
    nameHeaderElement.id = "nm-" + driver.id;
    nameHeaderElement.innerHTML = "Name: " + driver.name;

    const moneyElement = document.createElement("div");
    moneyElement.classList.add("money");
    moneyElement.id = "mon-" + driver.id;
    moneyElement.innerHTML = "Money: " + driver.money + "грн";

    const orderStatusElement = document.createElement("div");
    orderStatusElement.classList.add("status");
    orderStatusElement.id = "ord-" + driver.id;
    orderStatusElement.innerHTML = "Status: " + driver.operation.status;

    const carFuelCapacityElement = document.createElement("div");
    carFuelCapacityElement.classList.add("carFuelCapacity");
    carFuelCapacityElement.id = "ful-" + driver.id;
    carFuelCapacityElement.innerHTML = "Fuel: " + driver.car.fuelCapacity;

    driverElement.appendChild(nameHeaderElement);
    driverElement.appendChild(moneyElement);
    driverElement.appendChild(orderStatusElement);
    driverElement.appendChild(carFuelCapacityElement);

    infoMessegeElement.appendChild(driverElement);
}

const updateInfoDriver = (driver) => {
    const nameNewElement = document.getElementById("nm-" + driver.id);
    nameNewElement.innerHTML = "Name: " + driver.name;

    const moneyNewElement = document.getElementById("mon-" + driver.id);
    moneyNewElement.innerHTML = "Money: " + driver.money + "грн";

    const orderNewElement = document.getElementById("ord-" + driver.id);
    orderNewElement.innerHTML = "Status: " + driver.operation.status;

    const fuelNewElement = document.getElementById("ful-" + driver.id);
    fuelNewElement.innerHTML = "Fuel: " + driver.car.fuelCapacity;

}