const crown = '/jewelry';
let jewelryItems = [];

function getJewelryItems() {
    fetch(crown)
        .then(response => response.json())
        .then(data => _displayJewelryItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addJewelry() {
    const addNameTextbox = document.getElementById('add-name');
    const addWeightTextbox = document.getElementById('add-weight');

    const item = {
        name: addNameTextbox.value.trim(),
        weight: addWeightTextbox.value.trim()
    };

    fetch(crown, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getJewelryItems();
            addNameTextbox.value = '';
            addWeightTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteJewelry(id) {
    fetch(`${crown}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getJewelryItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = jewelryItems.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-weight').value = item.weight;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('editForm').style.display = 'block';
}

function updateJewelry() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        name: document.getElementById('edit-name').value.trim(),
        weight: document.getElementById('edit-weight').value.trim()
    };

    fetch(`${crown}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getJewelryItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayJewelryItems(data) {
    const tBody = document.getElementById('jewelry-list');
    tBody.innerHTML = '';

    _displayCount(data.length);

    data.forEach(item => {
        let editButton = document.createElement('button');
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = document.createElement('button');
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteJewelry(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(item.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(item.weight); // אם אין ערך, הצג "N/A"
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    jewelryItems = data;
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'item' : 'items';
    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}