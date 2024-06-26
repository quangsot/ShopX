import Cancel from '@/components/icons/Cancel'
import { GRAY_SCALE } from '@/components/layouts/WrapLayout/ModeTheme/colors'
import { ErrorCodes } from '@/service/type'
import CheckCircleOutlinedIcon from '@mui/icons-material/CheckCircleOutlined'
import CloseOutlinedIcon from '@mui/icons-material/CloseOutlined'
import { Divider, Typography } from '@mui/material'
import { useTranslation } from 'next-i18next'
import { toast } from 'react-toastify'

interface MessageProps {
  title?: string
  message: string
}

export const successMsg = (msg: string) => {
  if (msg)
    toast(<SuccessMessage message={msg} />, {
      closeButton: () => (
        <div className='px-12 my-auto border-l'>
          <CloseOutlinedIcon fontSize='small' />
        </div>
      ),
      className: 'vds-toast__success',
    })
}

export const errorMsg = (
  error: ErrorCodes[] | string | unknown,
  setError?: any
) => {
  if (Array.isArray(error) && error.length > 0) {
    error.map((item) => {
      if (item && Array.isArray(item.fields) && setError) {
        item.fields.map((ele: any) =>
          setError(ele, {
            type: 'be',
            message: item.message,
          })
        )
      } else errorMsg(item.message)
    })
  } else if (typeof error === 'string') {
    toast(<ErrorMessage message={error} />, {
      closeButton: () => (
        <div className='px-12 my-auto border-l'>
          <CloseOutlinedIcon fontSize='small' />
        </div>
      ),
      className: 'vds-toast__error',
    })
  } else
    toast(<ErrorMessage message='Có lỗi xảy ra' />, {
      className: 'vds-toast__error',
    })
}

const ErrorMessage = (props: MessageProps) => {
  const { message, title } = props
  const { t } = useTranslation('common')
  return (
    <div className='flex items-center'>
      <Cancel />
      <div className='px-6 vds-toast__msg' style={{ color: '#242424' }}>
        <Typography variant='subtitle2' className='mb-3'>
          {title ?? t('message.fail')}
        </Typography>
        <Typography variant='body2' style={{ color: '#747475' }}>
          {message}
        </Typography>
      </div>
      <Divider
        orientation='horizontal'
        color={GRAY_SCALE}
        style={{ width: 1 }}
      />
    </div>
  )
}

export const SuccessMessage = (props: MessageProps) => {
  const { message, title } = props
  const { t } = useTranslation('common')
  return (
    <div className='flex items-center'>
      <CheckCircleOutlinedIcon
        style={{ height: 30, width: 30 }}
        color='primary'
      />
      <div className='px-12 vds-toast__msg' style={{ color: '#242424' }}>
        <Typography variant='subtitle2' className='mb-3'>
          {title ?? t('message.success')}
        </Typography>
        <Typography variant='body2' style={{ color: '#747475' }}>
          {message}
        </Typography>
      </div>
    </div>
  )
}
