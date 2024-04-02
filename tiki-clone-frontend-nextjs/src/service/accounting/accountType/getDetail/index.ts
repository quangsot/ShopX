import { accountingApi } from '@/config/axios'
import { defaultOption } from '@/config/reactQuery'
import { useQuery } from 'react-query'
import { RequestParams, Response } from './type'

export const getAccountTypeDetail = async (
  params: RequestParams['GET']
): Promise<Response['GET']> => {
  const { data } = await accountingApi({
    method: 'get',
    url: '/api/v1/account-type',
    params,
  })
  return data
}

export const useQueryGetAccountTypeDetail = (
  params: RequestParams['GET'],
  options?: any
) => {
  return useQuery(
    ['/api/v1/account-type', params],
    () => getAccountTypeDetail(params),
    {
      ...defaultOption,
      ...options,
    }
  )
}
