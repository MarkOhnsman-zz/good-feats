<template>
  <div class="component container-fluid">
    <div class="row">
      <div class="col d-flex mt-5">
        <img :src="profile.picture" height="150" />
        <div>
          <h2 class="m-0">{{ profile.name }}</h2>
          <p class="m-0">{{ profile.email }}</p>
        </div>
      </div>
    </div>

    <div class="masonry">
      <div
        class="bg-primary darken-35 text-light m-1 p-1 brick"
        v-for="r in reviews"
        :key="r.id"
      >
        <h2>{{ r.body }}</h2>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { profilesService } from '../services/ProfilesService'
import { useRoute } from 'vue-router'
import { reviewsService } from '../services/ReviewsService'
import { AppState } from '../AppState'


export default {
  setup() {
    const route = useRoute()
    onMounted(() => {
      profilesService.getById(route.params.id)
      profilesService.getReviews(route.params.id)
    })
    return {
      profile: computed(() => AppState.activeProfile),
      reviews: computed(() => AppState.reviews)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry {
  columns: 8 20vw;
  column-gap: 1rem;
  .brick {
    display: inline-block;
    width: 100%;
  }
}
</style>  