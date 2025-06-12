// Base API URL
const API_URL = 'https://localhost:5001/api/ZipCodes';

// Load and display all ZIP records in the table
async function loadZipCodes() {
  const res = await fetch(API_URL);
  const zipCodes = await res.json();

  const tbody = document.querySelector('#zipTable tbody');
  tbody.innerHTML = '';

  zipCodes.forEach(zip => {
    const row = document.createElement('tr');
    row.innerHTML = `
      <td><input type="text" value="${zip.zip}" data-field="zip" /></td>
      <td><input type="text" value="${zip.city}" data-field="city" /></td>
      <td><input type="text" value="${zip.state}" data-field="state" /></td>
      <td><input type="text" value="${zip.county ?? ''}" data-field="county" /></td>
      <td><input type="number" step="any" value="${zip.latitude ?? ''}" data-field="latitude" /></td>
      <td><input type="number" step="any" value="${zip.longitude ?? ''}" data-field="longitude" /></td>
      <td>
        <button onclick="updateZip(${zip.id}, this)">Update</button>
        <button onclick="deleteZip(${zip.id})">Delete</button>
      </td>
    `;
    tbody.appendChild(row);
  });
}

// Delete ZIP record by ID
async function deleteZip(id) {
  await fetch(`${API_URL}/${id}`, { method: 'DELETE' });
  loadZipCodes();
}

// Update ZIP record with edited values from table row
async function updateZip(id, button) {
  const row = button.closest('tr');
  const inputs = row.querySelectorAll('input');

  const updatedZip = {};
  inputs.forEach(input => {
    const key = input.dataset.field;
    let value = input.value;

    if (key === 'latitude' || key === 'longitude') {
      value = value === '' ? null : parseFloat(value);
    }

    updatedZip[key] = value;
  });

  updatedZip.id = id; // Ensure ID is included

  await fetch(`${API_URL}/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(updatedZip)
  });

  loadZipCodes();
}

// Add new ZIP record from form
document.getElementById('zipForm').addEventListener('submit', async (e) => {
  e.preventDefault();

  const newZip = {
    zip: document.getElementById('zip').value,
    city: document.getElementById('city').value,
    state: document.getElementById('state').value,
    county: document.getElementById('county').value,
    latitude: parseFloat(document.getElementById('latitude').value) || null,
    longitude: parseFloat(document.getElementById('longitude').value) || null
  };

  await fetch(API_URL, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(newZip)
  });

  document.getElementById('zipForm').reset();
  loadZipCodes();
});

// Load table on page load
window.onload = loadZipCodes;