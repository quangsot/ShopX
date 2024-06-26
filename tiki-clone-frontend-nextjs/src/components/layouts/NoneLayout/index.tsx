import { DialogProvider } from '@/components/hooks/dialog/useDialog'
import { useAppSelector } from '@/redux/hook'
import { ThemeOptions, createTheme } from '@mui/material'
import NextNProgress from 'nextjs-progressbar'
import { ReactElement } from 'react'
import { QueryClient, QueryClientProvider } from 'react-query'
import { RecoilRoot } from 'recoil'
import ModeTheme from '../WrapLayout/ModeTheme'
import { GRAY_SCALE, RED } from '../WrapLayout/ModeTheme/colors'
import dynamic from 'next/dynamic'

const queryClient = new QueryClient()

const { NoneWrapLayout } = {
  NoneWrapLayout: dynamic(
    () =>
      import('@/components/layouts/NoneLayout/NoneWrapLayout/index').then(
        (component) => component.NoneWrapLayout
      ),
    {
      ssr: false,
      // loading: () => <>...Loading...</>,
    }
  ),
}

export const NoneLayout = (page: ReactElement) => {
  const mainTheme = useAppSelector((state) => state.themeColorData)
  const fontConfig = useAppSelector((state) => state.fontData)

  const palette = {
    primary: {
      main: mainTheme.firstMainColor,
      dark: mainTheme.firstMainColor,
    },
    secondary: {
      main: mainTheme.secondMainColor,
      dark: mainTheme.secondMainColor,
    },
    success: {
      main: mainTheme.thirdMainColor,
      dark: mainTheme.thirdMainColor,
    },
    error: {
      main: mainTheme.errorColor,
      light: mainTheme.errorColor,
      dark: mainTheme.errorColor,
    },
    warning: {
      main: mainTheme.warningColor,
      light: mainTheme.warningColor,
      dark: mainTheme.warningColor,
    },
  }

  const typography = {
    fontFamily: ['Helvetica', 'Roboto', 'sans-serif'].join(','),
    allVariants: {
      fontSize: '1rem',
    },
    h1: {
      fontSize: fontConfig.h1Size ? `${fontConfig.h1Size}rem` : '6rem',
      lineHeight: '1.75rem',
      color: fontConfig.h1Color,
      fontFamily: fontConfig.h1Font,
      fontWeight: 'light',
      '@media (max-width:640px)': {
        fontSize: '4.5rem',
      },
    },
    h2: {
      fontSize: fontConfig.h2Size ? `${fontConfig.h2Size}rem` : '3.75rem',
      lineHeight: fontConfig.h2Size ? `${fontConfig.h2Size}rem` : '5.625rem',
      color: fontConfig.h2Color,
      fontFamily: fontConfig.h2Font,
      fontWeight: 'light',
      '@media (max-width:640px)': {
        fontSize: '3.125rem',
      },
    },
    h3: {
      fontSize: fontConfig.h3Size ? `${fontConfig.h3Size}rem` : '1.875rem',
      lineHeight: fontConfig.h3Size ? `${fontConfig.h3Size}rem` : '3rem',
      color: fontConfig.h3Color,
      fontFamily: fontConfig.h3Font,
      fontWeight: 600,
      '@media (max-width:640px)': {
        fontSize: '1.75rem',
      },
    },
    h4: {
      fontSize: fontConfig.h4Size ? `${fontConfig.h4Size}rem` : '1.625rem',
      lineHeight: '1.25rem',
      color: fontConfig.h4Color,
      fontFamily: fontConfig.h4Font,
      fontWeight: 500,
      '@media (max-width:640px)': {
        fontSize: '1.5rem',
      },
    },
    h5: {
      fontSize: fontConfig.h5Size ? `${fontConfig.h5Size}rem` : '1.5rem',
      lineHeight: fontConfig.h5Size ? `${fontConfig.h5Size}rem` : '1.75rem',
      color: fontConfig.h5Color,
      fontFamily: fontConfig.h5Font,
      fontWeight: 500,
      '@media (max-width:640px)': {
        fontSize: '1.375rem',
      },
    },
    h6: {
      fontSize: fontConfig.h6Size ? `${fontConfig.h6Size}rem` : '1.1875rem',
      lineHeight: fontConfig.h6Size ? `${fontConfig.h6Size}rem` : '1.4375rem',
      color: fontConfig.h6Color,
      fontFamily: fontConfig.h6Font,
      fontWeight: 500,
      '@media (max-width:640px)': {
        fontSize: '1.0625rem',
      },
    },
    subtitle1: {
      fontSize: fontConfig.subtitle1Size
        ? `${fontConfig.subtitle1Size}rem`
        : '0.9375rem',
      lineHeight: fontConfig.subtitle1Size
        ? `${fontConfig.subtitle1Size}rem`
        : '1.25rem',
      color: fontConfig.subtitle1Color,
      fontFamily: fontConfig.subtitle1Font,
      fontWeight: 500,
      '@media (max-width:640px)': {
        fontSize: '0.875rem',
      },
    },
    subtitle2: {
      fontSize: fontConfig.subtitle2Size
        ? `${fontConfig.subtitle2Size}rem`
        : '0.8125rem',
      lineHeight: '1rem',
      color: fontConfig.subtitle2Color,
      fontFamily: fontConfig.subtitle2Font,
      fontWeight: 400,
      '@media (max-width:640px)': {
        fontSize: '0.75rem',
      },
    },
    body1: {
      fontSize: fontConfig.body1Size
        ? `${fontConfig.body1Size}rem`
        : '0.9375rem',
      lineHeight: fontConfig.body1Size
        ? `${fontConfig.body1Size}rem`
        : '1.25rem',
      color: fontConfig.body1Color,
      fontFamily: fontConfig.body1Font,
      fontWeight: 'normal',
      '@media (max-width:640px)': {
        fontSize: '0.875rem',
      },
    },
    body2: {
      fontSize: fontConfig.body2Size
        ? `${fontConfig.body2Size}rem`
        : '0.9375rem',
      lineHeight: fontConfig.body2Size
        ? `${fontConfig.body2Size}rem`
        : '1.25rem',
      color: fontConfig.body2Color,
      fontFamily: fontConfig.body2Font,
      '@media (max-width:640px)': {
        fontSize: '0.75rem',
      },
    },
    caption: {
      fontSize: fontConfig.captionSize
        ? `${fontConfig.captionSize}rem`
        : '0.9375rem',
      lineHeight: fontConfig.captionSize
        ? `${fontConfig.captionSize}rem`
        : '1.25rem',
      color: fontConfig.captionColor,
      fontFamily: fontConfig.captionFont,
      '@media (max-width:640px)': {
        fontSize: '0.75rem',
      },
    },
  }

  const themeConfig: ThemeOptions = {
    palette,
    typography,
    components: {
      MuiButton: {
        styleOverrides: {
          root: {
            margin: 'none',
            borderRadius: 6,
            ':hover': {
              opacity: 0.8,
            },
            minHeight: 36,
            ':disabled': {
              opacity: 0.5,
            },
          },
          text: {
            border: `1px solid ${GRAY_SCALE}`,
          },
        },
        defaultProps: {
          disableElevation: true,
          variant: 'contained' as any,
        },
      },
      MuiIconButton: {
        styleOverrides: {
          root: {
            padding: 4,
          },
        },
      },
      MuiInputLabel: {
        styleOverrides: {
          asterisk: {
            color: RED,
          },
        },
        defaultProps: {
          shrink: true,
          sx: {
            ...typography.subtitle2,
            color: '#7A7A7A',
            padding: '2px',
          },
        },
      },

      MuiInputBase: {
        styleOverrides: {
          input: {
            padding: '4px 4px 4px 0',
          },
        },
      },

      MuiOutlinedInput: {
        styleOverrides: {
          root: {
            minHeight: 48,
            borderColor: GRAY_SCALE,
          },
          input: {
            // padding: '14.663px',
          },
          notchedOutline: { borderColor: GRAY_SCALE },
        },
        defaultProps: {
          sx: {
            // ...typography.body2,
            borderRadius: '4px',
          },
        },
      },

      MuiInput: {
        defaultProps: {
          sx: {
            ...typography.h3,
            fontWeight: 400,
            padding: '2px',
          },
        },
      },
      MuiTextField: {
        defaultProps: {
          InputProps: {
            style: { minHeight: 48 },
          },
        },
        styleOverrides: {
          root: {
            // ...typography.body2,
          },
        },
      },
      MuiPopover: {},
      MuiPopper: {},
      MuiCardHeader: {
        styleOverrides: {
          action: {
            display: 'flex',
            flex: 1,
            justifyContent: 'flex-end',
            margin: 0,
          },
          content: {
            width: 'fit-content',
            flex: 'inherit',
          },
        },
      },
      MuiAutocomplete: {
        styleOverrides: {
          tag: {
            // ...typography.body2,
            margin: '1px 3px',
            padding: 0,
          },
          inputRoot: {
            paddingBottom: 2,
          },
          hasPopupIcon: { padding: 0 },
          hasClearIcon: { padding: 0 },
        },
      },

      MuiDialog: {
        styleOverrides: {
          container: {
            minWidth: 500,
          },
        },
      },
      MuiTableCell: {
        styleOverrides: {
          root: {
            fontSize: '0.875rem',
            padding: '0.8rem',
          },
        },
      },

      MuiFormHelperText: {
        styleOverrides: {
          root: {
            margin: '5px 0px',
          },
        },
      },
      MuiTypography: {
        styleOverrides: {
          root: {},
        },
      },

      MuiGrid: {
        styleOverrides: {
          root: {
            margin: 0,
            width: '100%',
          },
        },
      },
    },
  }

  const theme = createTheme(themeConfig)

  return (
    <QueryClientProvider client={queryClient}>
      <RecoilRoot>
        <ModeTheme theme={theme}>
          <NoneWrapLayout>
            <NextNProgress color={mainTheme.firstMainColor} height={4} />
            <DialogProvider>{page}</DialogProvider>
          </NoneWrapLayout>
        </ModeTheme>
      </RecoilRoot>
    </QueryClientProvider>
  )
}
