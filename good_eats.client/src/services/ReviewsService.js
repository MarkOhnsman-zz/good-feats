import { AppState } from '../AppState'
import { api } from './AxiosService'

class ReviewsService {
  async createReview(review) {
    const res = await api.post('api/reviews', review)
    console.log(res.data)
    AppState.reviews.push(res.data)
  }

  async deleteReview(id) {
    await api.delete('api/reviews/' + id)
    AppState.reviews = AppState.reviews.filter(r => r.id != id)
  }
}

export const reviewsService = new ReviewsService()