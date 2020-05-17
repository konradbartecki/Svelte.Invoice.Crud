<script>
  import { onMount, tick } from 'svelte';
  import InvoicesIndex from "./Invoices.Index.svelte";
  import InvoicesEdit from "./Invoices.Edit.svelte";
  import InvoicesNew from "./Invoices.New.svelte";

  let isEditing = false;
  let editedItem;
  let isCreating = false;
  let isLoading = false;
  let indexComponent;

  // onMount(async () => await onMountAsync());

  // async function onMountAsync() {
  //     await indexComponent.Refresh();
  // }

  async function handleMessage(event) {
    console.log(event);
    if (event.detail.type == "onInvoiceEdit") {
      editedItem = event.detail.invoice;
      isEditing = true;
    } else if (event.detail.type == "onInvoiceCreateStart"){
      isCreating = true;
    } else if (event.detail.type == "onInvoiceEditCancel") {
      editedItem = null;
      isEditing = false;
      // await onMountAsync();
    } else if (event.detail.type == "onInvoiceCreationDone") {
      isCreating = false;
      // await onMountAsync();
    }
  }
</script>

{#if isEditing}
  <InvoicesEdit {editedItem} on:message={handleMessage} />
{:else if isCreating}
  <InvoicesNew on:message={handleMessage}/>
{:else}
  <InvoicesIndex bind:this={indexComponent} on:message={handleMessage}/>
{/if}
