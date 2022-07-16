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

}

const addFuel = (refuiling) => {
    const fuelElement = document.createElement("div");
    fuelElement.classList.add("fuel");
    fuelElement.id = "f-" + refuiling.id;
    fuelElement.style.left = (refuiling.location * 0.5) + "%";
    fuelElement.innerHTML = refuiling.name;
    const ramdomValue = Math.random();
    if (ramdomValue > 0.5) {
        fuelElement.style.top = "20px";
    }
    else {
        fuelElement.style.top = "350px";
    } 
    currentRefuielingIds.push(refuiling.id);
    mapElement.appendChild(fuelElement);
}

const addOrder = (deliveryOrder) => {
    const clientElement = document.createElement("img");
    clientElement.classList.add("child");
    clientElement.id = "c-" + deliveryOrder.id;
    clientElement.src = "/img/05.png";
    clientElement.style.left = (deliveryOrder.client.location * 0.5) + "%";

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
    }
    else {
        driverElement.classList.add("car-right");
    }
}

const updateFuel = (refuiling) => {

}

const updateOrder = (deliveryOrder) => {
    if (deliveryOrder.statusOrder < 3) {
        document.getElementById("c-" + id).style.display = "none";
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
    }
    for (let driver of driversToAdd) {
        addDriver(driver);
    }
    for (let driverId of driverIdsToDelete) {
        deleteDriver(driverId);
    }
}

const updateMapFuels = (refuilings) => {
    const fuelsToUpdate = refuilings.filter(f => currentRefuielingIds.includes(f.id));
    const fuelsToAdd = refuilings.filter(f => !currentRefuielingIds.includes(f.id));

    const newFuelIds = refuilings.map(f => f.id);
    const fuelIdsToDelete = currentRefuielingIds.filter(id => !newFuelIds.includes(id));

    for (let refuiling of fuelsToUpdate) {
        updateFuel(refuiling);
    }
    for (let refuiling of fuelsToAdd) {
        addFuel(refuiling);
    }
    for (let refuilingId of fuelIdsToDelete) {
        deleteFuel(refuilingId);
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

const AddNewCarElement = () => {
    const carNewElement = document.createElement("div");
    carNewElement.classList.add("container-info");

    const carButtonAddElement = document.createElement("button");
    carButtonAddElement.classList.add("btn", "btn-add");
    carButtonAddElement.innerHTML = "Add";

    const carButtonDeleteElement = document.createElement("button");
    carButtonAddElement.classList.add("btn", "btn-delete");
    carButtonAddElement.innerHTML = "Delete";
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

const newArrayMap = () => {


}