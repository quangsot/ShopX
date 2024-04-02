import { accountingApi } from '@/config/axios'
import { RequestBody, Response } from './type'

export const deleteAccountLedger = async (
  params: RequestBody['DELETE']
): Promise<Response['DELETE']> => {
  const { data } = await accountingApi({
    method: 'delete',
    url: `/api/v1/account-ledger`,
    params,
  })
  return data
}
