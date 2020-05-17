<script>
    import { onMount } from "svelte";
    export let selectedClientId;
    let client;
    let isModalActive = false;
    let isCreationMode = false;

    let clients = [];

    function createNewClient(){
        client = {
            fullName: "John Doe Development",
            taxId: "PL123456789",
            country: "Poland",
            address: "Address",
            postalCode: "12-345"
        };
        isCreationMode = true;
        isModalActive = true;
    }

    function seeDetails() {
        
    }
</script>

<div class="field has-addons">
  <div class="control is-expanded">
    <div class="select is-fullwidth">
      <select name="client">

      </select>
    </div>
  </div>
  <div class="control">
    <button type="submit" class="button is-link">Details</button>
  </div>
  <div class="control">
    <button type="submit" class="button is-primary">New</button>
  </div>
</div>

{#if isModalActive}
<div class="modal is-active">
  <div class="modal-background"></div>
  <div class="modal-content">
    <div class="field">
      <label class="label">Full name</label>
      <div class="control">
        <input
          class="input"
          type="text"
          placeholder="Full name"
          bind:value={client.fullName}/>
      </div>
    </div>
    <div class="field">
      <label class="label">Tax ID</label>
      <div class="control">
        <input
          class="input"
          type="text"
          placeholder="PL12345679"
          bind:value={client.taxId}/>
      </div>
    </div>
    <div class="field">
      <label class="label">Country</label>
      <div class="control">
        <input
          class="input"
          type="text"
          placeholder="Poland"
          bind:value={client.country}/>
      </div>
    </div>
    <div class="field">
      <label class="label">Address</label>
      <div class="control">
        <input
          class="input"
          type="text"
          placeholder="Address"
          bind:value={client.address}/>
      </div>
    </div>
    <div class="field">
      <label class="label">Postal code</label>
      <div class="control">
        <input
          class="input"
          type="text"
          placeholder="Postal code"
          bind:value={client.postalCode}/>
      </div>
    </div>
    <div class="field is-grouped">
        {#if isCreationMode}
        <div class="control">
            <button class="button is-primary">Create</button>
        </div>
        {:else}
        <div class="control">
            <button class="button is-link">Save</button>
        </div>
        <div class="control">
            <button class="button is-danger">Delete</button>
        </div>
        {/if}
        <div class="control">
            <button class="button is-link is-light">Cancel</button>
        </div>
    </div>
</div>
</div>
{/if}