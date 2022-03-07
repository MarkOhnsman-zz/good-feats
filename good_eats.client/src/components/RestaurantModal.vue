<template>
  <div class="component d-flex">
    <img class="img-fluid w-50" :src="restaurant.picture" />
    <div class="p-3">
      <div>
        <h3>
          {{ restaurant.address }} | Rating:
          {{ restaurant.averageRating?.toFixed(1) }}
          <i class="mdi mdi-star text-warning"></i>
        </h3>
        <div class="reviews">
          <div class="text-end">
            <button
              class="btn btn-outline-success mdi mdi-plus"
              @click="createForm = true"
              v-if="!createForm && account.id"
            ></button>
            <form class="my-3" @submit.prevent="createReview" v-if="createForm">
              <div class="d-flex justify-content-end align-items-center">
                <p class="m-0 mx-3 selectable p-2" @click="createForm = false">
                  cancel
                </p>
                <button class="btn btn-success">create</button>
              </div>
              <div class="d-flex">
                <div class="mx-2">
                  <label class="mx-1" for="body">Review: </label>
                  <input
                    type="text"
                    placeholder="Review..."
                    v-model="newReview.body"
                    required
                    minlength="10"
                    maxlength="255"
                  />
                </div>
                <div class="mx-2">
                  <label class="mx-1" for="rating">{{
                    newReview.rating
                  }}</label>
                  <input
                    type="range"
                    min="1"
                    max="5"
                    class="slider"
                    v-model="newReview.rating"
                  />
                </div>
                <div class="mx-2">
                  <label class="mx-1" for="publish">Publish?</label>
                  <input type="checkbox" v-model="newReview.published" />
                </div>
              </div>
            </form>
          </div>
          <div
            class="p-3 bg-primary darken-35 m-3 text-light text-uppercase"
            v-for="r in reviews"
            :key="r.id"
          >
            <p class="m-0">
              "{{ r.body }}" -
              <span class="selectable" @click="goToProfile(r.creatorId)">
                {{ r.creator.name }}
              </span>
              ({{ r.rating }} <i class="mdi mdi-star"></i>)
            </p>
            <div class="text-end" v-if="r.creatorId == account.id">
              <i
                class="mdi mdi-delete selectable"
                @click="deleteReview(r.id)"
              ></i>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { reviewsService } from '../services/ReviewsService'
import { Modal } from 'bootstrap'
import { useRouter } from 'vue-router'

export default {
  setup() {
    const createForm = ref(false)
    const newReview = ref({
      rating: 1
    })
    const router = useRouter()
    return {
      createForm,
      newReview,
      restaurant: computed(() => AppState.activeRestaurant),
      reviews: computed(() => AppState.reviews),
      account: computed(() => AppState.account),
      async createReview() {
        try {
          const review = { ...newReview.value, restaurantId: AppState.activeRestaurant.id }
          await reviewsService.createReview(review)
        } catch (error) {
          logger.error(error)
          Pop.toast('Something Went Wrong!', 'error')
        }
      },
      async deleteReview(id) {
        try {
          await reviewsService.deleteReview(id)
        } catch (error) {
          logger.error(error)
          Pop.toast('Something went wrong')
        }
      },
      goToProfile(creatorId) {
        Modal.getOrCreateInstance(document.getElementById('restaurant-modal')).hide()
        router.push({ name: 'Profile', params: { id: creatorId } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
img {
  height: 60vh;
  object-fit: cover;
}
</style>