<template>
  <div class="restaurant p-4" @click="setActive">
    <div class="d-flex bg-white">
      <div class="flex-shrink-0">
        <img class="elevation-1" :src="restaurant.picture" height="100" />
      </div>
      <div class="flex-grow-1 d-flex elevation-1 ps-3">
        <div class="">
          <h3 class="m-0 border-bottom">{{ restaurant.name }}</h3>
          <p>
            <b>{{ restaurant.address }}</b>
          </p>
          <div class="d-flex">
            <p class="p-2 rounded bg-secondary">{{ restaurant.category }}</p>
          </div>
        </div>
        <h3 class="ms-3">
          {{ restaurant.averageRating.toFixed(1) }}
          <i
            class="mdi mdi-star text-warning"
            v-for="i in Math.floor(restaurant.averageRating)"
            :key="i"
          ></i>
          <i
            v-if="
              Math.floor(restaurant.averageRating) <
              Math.round(restaurant.averageRating)
            "
            class="mdi mdi-star-half text-warning"
          ></i>
          <span>({{ restaurant.totalReviews }})</span>
        </h3>
      </div>
    </div>
  </div>
</template>


<script>
import { Modal } from 'bootstrap'
import { AppState } from '../AppState'
import { restaurantsService } from '../services/RestaurantsService'
export default {
  props: {
    restaurant: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
      async setActive() {
        await restaurantsService.setActive(props.restaurant)
        Modal.getOrCreateInstance(document.getElementById('restaurant-modal')).toggle()
      }
    }
  }
}
</script>


<style lang="scss" scoped>
img {
  height: 130px;
  width: 130px;
  object-fit: cover;
}

.restaurant:hover {
  transform: scale(1.05);
  cursor: pointer;
}
</style>