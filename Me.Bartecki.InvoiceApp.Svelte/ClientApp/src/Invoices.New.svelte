<script>
  import InvoiceRowEditor from "./InvoiceRowEditor.svelte";
  import ClientSelector from "./ClientSelector.svelte";
  import { onMount } from "svelte";
  import { createEventDispatcher } from "svelte";
  const dispatch = createEventDispatcher();

  function dispatchDone() {
    dispatch("message", {
      type: "onInvoiceCreationDone"
    });
  }

  let invoiceRowEditor;
  let sellDate;
  let issueDate;
  let rows = [];
  let editedItem = {
    id: 0,
    clientName: "",
    clientId: 1,
    sellDate: new Date().toISOString().split("T")[0],
    issueDate: new Date().toISOString().split("T")[0],
    paytime: 14,
    isPaid: false
  };
  let isSaving = false;

  onMount(async () => {
    sellDate = new Date().toISOString().split("T")[0];
    issueDate = new Date().toISOString().split("T")[0];
    invoiceRowEditor.addNewRow();
  });

  async function submitSave() {
    isSaving = true;
    editedItem.sellDate = sellDate;
    editedItem.issueDate = issueDate;
    editedItem.invoiceRows = rows;
    await fetch(`/api/invoices/`, {
      method: "POST",
      body: JSON.stringify(editedItem),
      headers: {
        "Content-Type": "application/json"
      }
    }).then(function(response) {
      dispatchDone();
    });
    isSaving = false;
  }
</script>

<section class="hero is-primary is-bold">
  <div class="hero-body">
    <div class="container">
      <h1 class="title">
        <button class="button is-rounded" on:click={dispatchDone}>
          <span class="icon is-small">
            <i class="fas fa-chevron-left" />
          </span>
          <span>Back</span>
        </button>
        Invoices
        <button
          class="button is-rounded is-link is-pulled-right"
          on:click={submitSave}>
          <span>Save</span>
          <span class="icon is-small">
            <i class="fas fa-save" />
          </span>
        </button>
      </h1>
      <h2 class="subtitle">Creating new invoice</h2>
    </div>
  </div>
</section>
<section>
  <div class="container is-fluid">
    <div class="field">
      <label class="label">Client</label>
      <div class="control">
        <ClientSelector bind:selectedClientId={editedItem.clientId}/>
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
      <label class="label">Payment type</label>
      <div class="control">
        <div class="select">
          <select bind:value={editedItem.paymentType}>
            <option value="1">Cash</option>
            <option value="2">Bank transfer</option>
            <option value="3">Card payment</option>
          </select>
        </div>
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
          <input type="checkbox" bind:checked={editedItem.isPaid} />
          Is paid
        </label>
      </div>
    </div>
    <InvoiceRowEditor
      bind:this={invoiceRowEditor}
      editedInvoiceId={0}
      bind:rows />
  </div>
</section>
