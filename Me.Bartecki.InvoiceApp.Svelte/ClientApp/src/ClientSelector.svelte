<script>
  import { onMount } from "svelte";
  export let selectedClientId;
  let client;
  let isModalActive = false;
  let isCreationMode = false;
  let isDeleting = false;
  let isCreating = false;
  let isUpdating = false;

  let clients;

  async function Refresh() {
    await fetch(`/api/clients`).then(async function(response) {        
        clients = await response.json();
        console.log(selectedClientId);
        selectedClientId = selectedClientId;
    });
  }

  onMount(async () => await Refresh());

  async function createNewClient() {
    isCreating = true;
    var response = await fetch(`/api/clients/`, {
      method: "POST",
      body: JSON.stringify(client),
      headers: {
        "Content-Type": "application/json"
      }
    })
      .then(async function(response) {
        var json = await response.json();
        await Refresh();
        selectedClientId = json.id;
      })
      .finally(() => {
        isCreating = false;
        isModalActive = false;
      });
  }

  function showDetailsModal() {
    client = clients.filter(obj => {
      return obj.id === selectedClientId;
    })[0];
    isModalActive = true;
    isCreationMode = false;
  }

  function showCreateModal() {
    client = {
      fullName: "",
      taxId: "",
      country: "",
      address: "",
      postalCode: ""
    };
    isModalActive = true;
    isCreationMode = true;
  }

  async function deleteClient() {
    var response = await fetch(`/api/clients/` + selectedClientId, {
      method: "DELETE"
    })
    .then(async function(response) {
        selectedClientId = clients[0];
        await Refresh();
    })
    .finally(() => {
      isModalActive = false;
      isDeleting = false;
    });
  }

  async function updateClient() {
    await fetch(`/api/clients/` + selectedClientId, {
      method: "PUT",
      body: JSON.stringify(client),
      headers: {
        "Content-Type": "application/json"
      }
    })
    .then(async function(response){
        await Refresh();
    })
    .finally(() => {
        isUpdating = false;
    });
  }
</script>

<div class="field has-addons">
  <div class="control is-expanded">
    <div class="select is-fullwidth">
      {#if clients}
      <select name="client" bind:value={selectedClientId}>
          {#each clients as client}
            <option value={client.id}>{client.fullName}</option>
          {/each}
      </select>
      {/if}
    </div>
  </div>
  <div class="control">
    <button type="submit" class="button is-link" on:click={showDetailsModal}>
      Details
    </button>
  </div>
  <div class="control">
    <button type="submit" class="button is-primary" on:click={showCreateModal}>
      New
    </button>
  </div>
</div>

{#if client && isModalActive}
  <div class="modal is-active">
    <div class="modal-background" />
    <div class="modal-content">
      <div class="notification">

        <div class="field">
          <label class="label">Full name</label>
          <div class="control">
            <input
              class="input"
              type="text"
              placeholder="Full name"
              bind:value={client.fullName} />
          </div>
        </div>
        <div class="field">
          <label class="label">Tax ID</label>
          <div class="control">
            <input
              class="input"
              type="text"
              placeholder="PL12345679"
              bind:value={client.taxId} />
          </div>
        </div>
        <div class="field">
          <label class="label">Country</label>
          <div class="control">
            <input
              class="input"
              type="text"
              placeholder="Poland"
              bind:value={client.country} />
          </div>
        </div>
        <div class="field">
          <label class="label">Address</label>
          <div class="control">
            <input
              class="input"
              type="text"
              placeholder="Address"
              bind:value={client.address} />
          </div>
        </div>
        <div class="field">
          <label class="label">Postal code</label>
          <div class="control">
            <input
              class="input"
              type="text"
              placeholder="Postal code"
              bind:value={client.postalCode} />
          </div>
        </div>
        <div class="field is-grouped">
          {#if isCreationMode}
            <div class="control">
              <button
                class="button is-primary"
                on:click={createNewClient}
                class:is-loading={isCreating}>
                Create
              </button>
            </div>
          {:else}
            <div class="control">
              <button
                class="button is-link"
                on:click={updateClient}
                class:is-loading={isUpdating}>
                Save
              </button>
            </div>
            <div class="control">
              <button
                class="button is-danger"
                class:is-loading={isDeleting}
                on:click={deleteClient(selectedClientId)}>
                Delete
              </button>
            </div>
          {/if}
          <div class="control">
            <button
              class="button is-link is-light"
              on:click={() => (isModalActive = false)}>
              Cancel
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
{/if}
