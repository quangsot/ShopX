import { accountingApi } from '@/config/axios'
import { defaultOption } from '@/config/reactQuery'
import { useQuery } from 'react-query'
import { RequestBody, Response } from './type'

export const getTotalMoneyDebt = async (
  params: RequestBody['GET']
): Promise<Response['GET']> => {
  const { data } = await accountingApi({
    method: 'get',
    url: 'api/v1/debt/total-money-debt',
    params,
  })

  return data
}

export const useQueryGetTotalMoneyDebt = (
  params: RequestBody['GET'],
  options?: any
) => {
  return useQuery<Response['GET']>(
    ['api/v1/debt/total-money-debt', params],
    () => getTotalMoneyDebt(params),
    { ...defaultOption, ...options }
  )
}
