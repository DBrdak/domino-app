import React, { useEffect } from 'react'
import NavbarLayout from '../components/NavbarLayout'
import { useStore } from '../../global/stores/store'
import { observer } from 'mobx-react-lite'

function OnlineShopPanel() {
  const {adminLayoutStore, adminProductStore} = useStore()

  useEffect(() => {
    adminProductStore.loadProducts()
  }, [])

  return (
    <NavbarLayout>
      {adminLayoutStore.content.body}
    </NavbarLayout>
  )
}

export default observer(OnlineShopPanel)