<template>
  <div class="container">
    <div class="row justify-content-center">
      <div class="col-md-9" v-for="r in restaurants" :key="r.id">
        <Restaurant :restaurant="r" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted } from '@vue/runtime-core'
import { restaurantsService } from '../services/RestaurantsService'

export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      await restaurantsService.getAll()
    })

    return {
      restaurants: computed(() => AppState.restaurants)
    }
  }
}
</script>

<style scoped lang="scss">
</style>
