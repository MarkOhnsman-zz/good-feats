<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <Modal id="restaurant-modal">
    <template #modal-title>{{ restaurant.name }}</template>
    <template #modal-body>
      <RestaurantModal />
    </template>
  </Modal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState),
      restaurant: computed(() => AppState.activeRestaurant)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
</style>
