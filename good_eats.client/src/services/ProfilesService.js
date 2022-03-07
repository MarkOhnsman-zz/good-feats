import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {
  async getById(id) {
    const res = await api.get('api/profiles/' + id)
    logger.log(res.data)
    AppState.activeProfile = res.data
  }

  async getReviews(id) {
    const res = await api.get('api/profiles/' + id + '/reviews')
    logger.log(res.data)
    AppState.reviews = res.data
  }
}

export const profilesService = new ProfilesService()