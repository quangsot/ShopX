import { ButtonCustom } from '@/components/atoms/ButtonCustom'
import { CurrencyFormatCustom } from '@/components/atoms/CurrencyFormatCustom'
import { RED } from '@/components/layouts/WrapLayout/ModeTheme/colors'
import { AccountMoveDetail } from '@/service/accounting/accountMove/getDetail/type'
import { AccountOverPayment } from '@/service/accounting/accountMoveLineMatching/overPayment/type'
import { Typography } from '@mui/material'
import useMoneyBalanceItem from './useMoneyBalanceItem'

export type Props = {
  item: AccountOverPayment
  dataInvoice: AccountMoveDetail
  type: 'INBOUND' | 'OUTBOUND'
  refetch: any
}

const MoneyBalanceItem = ({ item, dataInvoice, type, refetch }: Props) => {
  const [_, handles] = useMoneyBalanceItem({
    item,
    dataInvoice,
    type,
    refetch,
  })
  const { onSubmit } = handles

  return (
    <div className='flex flex-row-reverse'>
      <div className='flex items-center min-w-[200px] flex-row-reverse'>
        <CurrencyFormatCustom
          amount={item.amount}
          color={RED}
          showCurrencyName
        />
      </div>

      <div className='flex items-center pl-10 min-w-[200px] flex-row-reverse'>
        <Typography color='primary' variant='subtitle1'>
          {item.accountPaymentCode}
        </Typography>
      </div>

      <ButtonCustom
        theme='draft'
        textTransform='none'
        height={38}
        onClick={onSubmit}
      >
        Thêm
      </ButtonCustom>
    </div>
  )
}

export default MoneyBalanceItem
