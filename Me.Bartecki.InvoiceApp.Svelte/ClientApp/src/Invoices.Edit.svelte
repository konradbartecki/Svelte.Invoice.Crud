<script>
  export let editedItem;

  import InvoiceRowEditor from "./InvoiceRowEditor.svelte";

  import { onMount } from "svelte";

  import { createEventDispatcher } from "svelte";
  const dispatch = createEventDispatcher();

  function dispatchCancel() {
    console.log("dispatching cancel");
    dispatch("message", {
      type: "onInvoiceEditCancel"
    });
  }

  let sellDate;
  let issueDate;
  let rows;
  let isSaving = false;

  onMount(async () => {
    sellDate = new Date(editedItem.sellDate).toISOString().split("T")[0];
    issueDate = new Date(editedItem.issueDate).toISOString().split("T")[0];
  });

  async function submitEdit() {
    isSaving = true;
    editedItem.sellDate = sellDate;
    editedItem.issueDate = issueDate;
    editedItem.invoiceRows = rows;
    await fetch('/api/invoiceRows/' + editedItem.id,{
        method: 'PUT', 
        body: JSON.stringify(rows),     
        headers: {
          'Content-Type': 'application/json'
          }})
      .then(async() => await fetch(`/api/invoices/` + editedItem.id, 
      { 
        method: 'PUT', 
        body: JSON.stringify(editedItem),     
        headers: {
          'Content-Type': 'application/json'
          }})
      .then(dispatchCancel()));      
    isSaving = false;
  }
</script>

<section class="hero is-primary is-bold">
  <div class="hero-body">
    <div class="container">
      <h1 class="title">
        <button class="button is-rounded" on:click={dispatchCancel}>
          <span class="icon is-small">
            <i class="fas fa-chevron-left" />
          </span>
          <span>Back</span>
        </button>
        Invoices
        <button class="button is-rounded is-link is-pulled-right" on:click={submitEdit}>
         <span>Save</span>
          <span class="icon is-small">
            <i class="fas fa-save" />
          </span>          
        </button>
      </h1>
      <h2 class="subtitle">
        Editing {editedItem.year}/{editedItem.month}/{editedItem.id} for {editedItem.clientName}
      </h2>
    </div>
  </div>
</section>
<section>
  <div class="container is-fluid">
    <div class="field">
      <label class="label">ID</label>
      <div class="control">
        <input
          class="input"
          type="text"
          placeholder="Text input"
          bind:value={editedItem.id}
          disabled />
      </div>
    </div>
    <div class="field">
      <label class="label">Client</label>
      <div class="control">
        <input
          class="input"
          type="text"
          placeholder="Text input"
          bind:value={editedItem.clientName} />
      </div>
    </div>
    <div class="field">
      <label class="label">Sold date</label>
      <div class="control">
        <input class="input" type="date" bind:value={sellDate} />
      </div>
    </div>
    <div class="field">
      <label class="label">Issued date</label>
      <div class="control">
        <input class="input" type="date" bind:value={issueDate} />
      </div>
    </div>
    <div class="field">
      <label class="label">Pay time (days)</label>
      <div class="control">
        <input class="input" type="text" bind:value={editedItem.paytime} />
      </div>
    </div>
    <div class="field">
      <div class="control">
        <label class="checkbox">
          <input type="checkbox" bind:value={editedItem.isPaid} />
          Is paid
        </label>
      </div>
    </div>
    <InvoiceRowEditor editedInvoiceId={editedItem.id} bind:rows={rows}/>
  </div>
</section>
