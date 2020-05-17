<script>
  import { onMount } from "svelte";
  import { createEventDispatcher } from "svelte";
  import InPlaceEditor from "./InPlaceEditor.svelte";

  const dispatch = createEventDispatcher();

  export let editedInvoiceId;

  export let rows;

  onMount(async () => {
    if (editedInvoiceId != 0) {
      rows = await fetch(`/api/invoiceRows/` + editedInvoiceId).then(r =>
        r.json()
      );
    }
  });

  export function addNewRow() {
    rows.push({
      id: null,
      invoiceId: editedInvoiceId,
      invoice: null,
      itemName: "Item name",
      unitName: "unit",
      unitPrice: 123,
      amount: 123,
      vatRatePercent: 23
    });
    //hack: trigger list refresh
    rows = rows;
  }
</script>

<div class="container is-fluid notification">
  <h2 class="subtitle">
    Rows
    <button
      class="button is-primary is-rounded is-pulled-right"
      on:click={addNewRow}>
      <span class="icon is-small">
        <i class="fas fa-plus" />
      </span>
      <span>Add</span>
    </button>
  </h2>
  {#if rows}
    <table
      class="table is-bordered is-striped is-narrow is-hoverable is-fullwidth">
      <thead>
        <tr>
          <th>Pos.</th>
          <th>Item</th>
          <th>Amount</th>
          <th>Unit</th>
          <th>Unit net price</th>
          <th>VAT Tax rate</th>
          <th>Total net</th>
          <th>Total gross</th>
        </tr>
      </thead>
      <tbody>
        {#each rows as row, index}
          <tr>
            <td>{index + 1}</td>
            <td>
              <InPlaceEditor bind:value={row.itemName} />
            </td>
            <td>
              <InPlaceEditor bind:value={row.amount} />
            </td>
            <td>
              <InPlaceEditor bind:value={row.unitName} />
            </td>
            <td>
              <InPlaceEditor bind:value={row.unitPrice} /> PLN
            </td>
            <td>
              <InPlaceEditor bind:value={row.vatRatePercent} /> %
            </td>
            <td>{(row.amount * row.unitPrice).toFixed(2)} PLN</td>
            <td>
              {(row.amount * row.unitPrice + (row.amount * row.unitPrice * row.vatRatePercent) / 100).toFixed(2)} PLN
              <a
                class="delete is-pulled-right"
                on:click={() => {
                  rows.splice(index, 1);
                  rows = rows;
                }} />
            </td>
          </tr>
        {/each}
      </tbody>
    </table>
  {:else}
    <p>Loading...</p>
  {/if}
</div>
