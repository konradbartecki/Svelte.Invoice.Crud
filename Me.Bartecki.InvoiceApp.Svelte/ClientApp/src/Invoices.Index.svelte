<script>
  import { onMount, tick, afterUpdate } from "svelte";
  import { createEventDispatcher } from "svelte";
  const dispatch = createEventDispatcher();

  let invoices;

  export async function Refresh() {
    invoices = await fetch(`/api/invoices`).then(r => r.json());
  }

  function dispatchEdit(invoice) {
    console.log("dispatching edit");
    dispatch("message", {
      type: "onInvoiceEdit",
      invoice: invoice
    });
  }
  function dispatchNew() {
    dispatch("message", {
      type: "onInvoiceCreateStart"
    });
  }

  onMount(async () => {
    await Refresh();
    console.log("index on mount");
  });

  async function Delete(id) {
    await fetch(`/api/invoices/` + id, { method: "DELETE" }).then(
      async function(response) {
        await Refresh();
      }
    );
  }

  function PaymentTypeToString(t) {
    switch (t) {
      case 1:
        return "Cash";
      case 2:
        return "Bank transfer";
      case 3:
        return "Card payment";
      default:
        return "Unknown";
    }
  }
</script>

<section class="hero is-primary is-bold">
  <div class="hero-body">
    <div class="container">
      <h1 class="title">
        Invoices
        <button
          class="button is-rounded is-link is-pulled-right"
          on:click={dispatchNew}>
          <span>Add</span>
          <span class="icon is-small">
            <i class="fas fa-plus" />
          </span>
        </button>
      </h1>
      <h2 class="subtitle">List of all invoices</h2>
    </div>
  </div>
</section>
<section>
  <div class="container is-fluid notification">
    {#if invoices}
      <table
        class="table is-bordered is-striped is-narrow is-hoverable is-fullwidth">
        <thead>
          <tr>
            <th>Id</th>
            <th>Client</th>
            <th>Sold date</th>
            <th>Issued date</th>
            <th>Pay time</th>
            <th>Payment type</th>
            <th>Is paid</th>
            <th>Net total</th>
            <th>Gross total</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {#each invoices as inv}
            <tr>
              <td>{inv.year}/{inv.month}/{inv.id}</td>
              <td>{inv.clientName}</td>
              <td>{new Date(inv.sellDate).toLocaleDateString('pl')}</td>
              <td>{new Date(inv.issueDate).toLocaleDateString('pl')}</td>
              <td>{inv.paytime} days</td>
              <td>{PaymentTypeToString(inv.paymentType)}</td>
              <td>
                <div class="field">
                  <div class="control">
                    <label class="checkbox">
                      <input
                        type="checkbox"
                        checked={inv.isPaid}
                        onclick="return false;" />
                    </label>
                  </div>
                </div>
              </td>
              <td>{inv.netTotal.toFixed(2)} PLN</td>
              <td>{inv.grossTotal.toFixed(2)} PLN</td>
              <td>
                <div class="field has-addons is-pulled-right">
                  <p class="control">
                    <button class="button is-link" on:click={dispatchEdit(inv)}>
                      <span class="icon is-small">
                        <i class="far fa-edit" />
                      </span>
                    </button>
                  </p>
                  <p class="control">
                    <button class="button is-danger" on:click={Delete(inv.id)}>
                      <span class="icon is-small">
                        <i class="fas fa-trash-alt" />
                      </span>
                    </button>
                  </p>
                </div>
              </td>
            </tr>
          {/each}
        </tbody>
      </table>
    {:else}
      <progress class="progress is-small is-link" max="100" />
    {/if}
  </div>
</section>
