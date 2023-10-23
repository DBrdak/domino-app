import React from 'react'
import NavbarLayout from '../components/NavbarLayout'
import { useStore } from '../../global/stores/store'
import { observer } from 'mobx-react-lite'

function ShopsPanel() {
  const {adminLayoutStore} = useStore()

  return (
    <NavbarLayout>
      {adminLayoutStore.content.body}
    </NavbarLayout>
  )
}

export default observer(ShopsPanel)