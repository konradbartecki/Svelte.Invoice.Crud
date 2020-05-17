<script>
  let isEditing = false;
  export let value;
</script>

{#if isEditing}
  <div class="field has-addons">
    <div class="control">
      <input class="input" type="text" bind:value />
    </div>
    <div class="control">
      <a class="button is-primary" on:click={() => (isEditing = false)}>
        <span class="icon is-small">
          <i class="fas fa-save" />
        </span>
      </a>
    </div>
  </div>
{:else}
  <u on:click={() => (isEditing = true)}>{value}</u>
{/if}
