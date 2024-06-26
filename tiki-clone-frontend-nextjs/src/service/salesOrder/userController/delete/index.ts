import { authUAAAPI } from '@/config/axios'
import { RequestBody, Response } from './type'

export const deleteUser = async (
  requestBody: RequestBody['DELETE']
): Promise<Response['DELETE']> => {
  const { data } = await authUAAAPI({
    method: 'delete',
    url: 'api/v1/user',
    params: { userId: requestBody.id },
    data: requestBody,
  })

  return data ? data.data : data
}
