import DefaultImageApp from '@/assets/png/customer.png'
import ArrowsIcon from '@/assets/svg/arrows.svg'
import { WHITE } from '@/components/layouts/WrapLayout/ModeTheme/colors'
import { MAX_VALUE } from '@/helper/contain'
import { useAppSelector } from '@/redux/hook'
import { useQueryGetSystemList } from '@/service/uaa/system/getList'
import { System } from '@/service/uaa/system/getList/type'

import {
  Box,
  CircularProgress,
  Dialog,
  DialogContent,
  DialogTitle,
  Typography,
} from '@mui/material'
import Image from 'next/image'
import { useCallback, useState } from 'react'

const SystemAppBox = (props: any) => {
  const { item, handleChangeBizzApp } = props

  return (
    <Box
      className='flex flex-col items-center'
      sx={{
        ':hover': {
          cursor: 'pointer',
        },
      }}
      onClick={() => handleChangeBizzApp(item)}
    >
      <Image
        width={150}
        height={150}
        style={{ objectFit: 'cover', borderRadius: 8 }}
        src={item.imageUrl ?? DefaultImageApp}
        alt={item?.name}
        priority
      />

      <Typography
        style={{
          marginTop: 12,
        }}
      >
        {item?.name}
      </Typography>
    </Box>
  )
}

export const useSwitchSystem = () => {
  const { firstMainColor: PRIMARY } = useAppSelector(
    (state) => state.themeColorData
  )
  const [open, setOpen] = useState(false)

  const handleOpenDialog = () => setOpen(true)
  const handleCloseDialog = () => setOpen(false)

  const { data: systemList, isLoading: isLoadingSystem } =
    useQueryGetSystemList(
      {
        page: 0,
        size: MAX_VALUE,
      },
      {
        enabled: open,
      }
    )

  const handleChangeSystem = useCallback(
    (system: System) => {
      if (system?.homepage) {
        window.location.href = system.homepage
      }
      if (open) {
        handleCloseDialog()
      }
    },
    [open]
  )

  const renderDialogChoseBizzApp = () => {
    return (
      <Dialog
        open={open}
        onClose={() => setOpen(false)}
        PaperProps={{
          style: { overflow: 'inherit', borderRadius: 8 },
        }}
      >
        <DialogTitle
          style={{
            backgroundColor: PRIMARY,
            color: WHITE,
            borderTopLeftRadius: 8,

            borderTopRightRadius: 8,
          }}
          className='relative h-30'
        >
          <Box>
            <Box
              className='flex justify-center w-full'
              style={{ position: 'absolute', top: '-50%', left: 0, right: 0 }}
            >
              <Box className='bg-white rounded-full p-10'>
                <Image src={ArrowsIcon} alt='' />
              </Box>
            </Box>
          </Box>
        </DialogTitle>
        <DialogContent>
          {isLoadingSystem ? (
            <div className='min-h-[500px] min-w-[500px] flex justify-center items-center'>
              <CircularProgress />
            </div>
          ) : (
            <Box className='grid grid-cols-3 gap-15 mx-10 my-20'>
              {(systemList ? systemList.data.content : []).map(
                (item, index) => {
                  return (
                    <SystemAppBox
                      item={item}
                      key={index}
                      handleChangeBizzApp={handleChangeSystem}
                    />
                  )
                }
              )}
            </Box>
          )}
        </DialogContent>
      </Dialog>
    )
  }

  return { renderDialogChoseBizzApp, handleOpenDialog, handleCloseDialog }
}
