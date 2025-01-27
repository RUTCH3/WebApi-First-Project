const crown = "/jewelry";
let jewelryItems = [];

function getJewelryItems() {
  fetch(crown)
    .then((response) => response.json())
    .then((data) => _displayJewelryItems(data))
    .catch((error) => console.error("Unable to get items.", error));
}

function addJewelry() {
  const name = document.getElementById("add-name").value.trim();
  const weight = document.getElementById("add-weight").value.trim();

  const item = { name, weight };

  fetch(crown, {
    method: "POST",
    headers: { Accept: "application/json", "Content-Type": "application/json" },
    body: JSON.stringify(item),
  })
    .then(() => {
      getJewelryItems();
      document.getElementById("add-form").reset();
    })
    .catch((error) => console.error("Unable to add item.", error));
}

function deleteJewelry(id) {
  fetch(`${crown}/${id}`, { method: "DELETE" })
    .then(() => getJewelryItems())
    .catch((error) => console.error("Unable to delete item.", error));
}

function displayEditForm(id) {
  const item = jewelryItems.find((item) => item.id === id);
  document.getElementById("edit-id").value = item.id;
  document.getElementById("edit-name").value = item.name;
  document.getElementById("edit-weight").value = item.weight;
  document.getElementById("editForm").style.display = "block";
}

function updateJewelry() {
  const id = parseInt(document.getElementById("edit-id").value, 10);
  const name = document.getElementById("edit-name").value.trim();
  const weight = document.getElementById("edit-weight").value.trim();

  const item = { id, name, weight };

  fetch(`${crown}/${id}`, {
    method: "PUT",
    headers: { Accept: "application/json", "Content-Type": "application/json" },
    body: JSON.stringify(item),
  })
    .then(() => getJewelryItems())
    .catch((error) => console.error("Unable to update item.", error));

  closeInput();
}

function closeInput() {
  document.getElementById("editForm").style.display = "none";
}

function _displayJewelryItems(data) {
  const tBody = document.getElementById("jewelry-list");
  tBody.innerHTML = "";

  _displayCount(data.length);

  data.forEach((item) => {
    const tr = tBody.insertRow();

    const td1 = tr.insertCell(0);
    td1.textContent = item.name;

    const td2 = tr.insertCell(1);
    td2.textContent = item.weight || "N/A";

    const td3 = tr.insertCell(2);
    const editButton = document.createElement("button");
    editButton.textContent = "Edit";
    editButton.onclick = () => displayEditForm(item.id);
    td3.appendChild(editButton);

    const td4 = tr.insertCell(3);
    const deleteButton = document.createElement("button");
    deleteButton.textContent = "Delete";
    deleteButton.onclick = () => deleteJewelry(item.id);
    td4.appendChild(deleteButton);
  });

  jewelryItems = data;
}

function _displayCount(count) {
  const counter = document.getElementById("counter");
  counter.textContent = `${count} ${count === 1 ? "item" : "items"}`;
}
